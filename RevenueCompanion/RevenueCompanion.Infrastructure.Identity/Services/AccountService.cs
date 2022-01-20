using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RevenueCompanion.Application.DTOs.Account;
using RevenueCompanion.Application.DTOs.MenuSetup;
using RevenueCompanion.Application.Exceptions;
using RevenueCompanion.Application.Helpers;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using RevenueCompanion.Domain.Entities;
using RevenueCompanion.Domain.Settings;
using RevenueCompanion.Infrastructure.Identity.Helpers;
using RevenueCompanion.Infrastructure.Identity.Models;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly ApplicationDbContext _context;
        private readonly IMenuRepository _menuRepository;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IAuthenticatedUserService authenticatedUserService,
            ApplicationDbContext context, IMenuRepository menuRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            this._emailService = emailService;
            _authenticatedUserService = authenticatedUserService;
            _context = context;
            _menuRepository = menuRepository;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new Response<AuthenticationResponse> { Data = null, Succeeded = false, Message = $"Invalid Credentials for '{request.Email}'.", ResponseCode = "-1", StatusCode = 401 };
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return new Response<AuthenticationResponse> { Data = null, Succeeded = false, Message = $"Invalid Credentials for '{request.Email}'.", ResponseCode = "-1", StatusCode = 401 };
            }
            if (!user.IsActive)
            {
                return new Response<AuthenticationResponse> { Data = null, Succeeded = false, Message = $"Account for '{request.Email}' is inactive.", ResponseCode = "-1", StatusCode = 401 };
            }
            if (!user.EmailConfirmed)
            {
                return new Response<AuthenticationResponse> { Data = null, Succeeded = false, Message = $"Account for '{request.Email}' is inactive.", ResponseCode = "-1", StatusCode = 401 };
            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.MerchantCode = user.MerchantCode;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.FullName = user.FirstName != null ? user.FirstName : "" + user.LastName != null ? user.LastName : "";
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var userWithSameUserName = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameUserName != null)
            {
                return new Response<string> { Data = null, Message = $"Email '{request.Email}' is already taken.", ResponseCode = "-1", Succeeded = false, StatusCode = 400 };
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                EmailConfirmed = false,
                MerchantCode = request.MerchantCode,
                IsActive = false,
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var role = new IdentityRole();


                    role = await _roleManager.FindByIdAsync(request.RoleId);

                    await _userManager.AddToRoleAsync(user, role.Name);

                    //get the default menusetup ids 
                    var menusetupIds = await _context.MenuSetup.Where(c => c.MenuId == "UserDashboard" && c.IsGeneral).Select(c => c.MenuSetupId).ToListAsync();

                    //Assign the dashboard page automatically to the user
                    foreach (var menusetupId in menusetupIds)
                    {
                        var defaultPage = new UsersRolePermission
                        {
                            MenuSetupId = menusetupId,
                            MerchantCode = request.MerchantCode,
                            UserId = user.Id,
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            CreatedBy = _authenticatedUserService.UserId,
                        };
                        await _context.UsersRolePermission.AddAsync(defaultPage);
                        await _context.SaveChangesAsync();


                    }
                    var verificationUri = await SendVerificationEmail(user, request.WebUrl);

                    //TODO: Attach Email Service here and configure it via appsettings
                    var emailTemplate = new EmailHelper();
                    var msgBody = EmailHelper.ConfirmEmailTemplate(user.Email, verificationUri);

                    try
                    {
                        //try to send email
                        _emailService.SendEmail(user.Email, user.LastName, "Account Activation", msgBody);
                    }
                    catch (Exception ex)
                    {
                        // failed sending email

                    }
                    return new Response<string> { Data = "User created", Message = $"User Registered. Please confirm your account by visiting this URL {verificationUri}", ResponseCode = "00", Succeeded = true, StatusCode = 200 };
                }
                else
                {
                    return new Response<string> { Data = null, Message = result.Errors.FirstOrDefault().Description, ResponseCode = "-2", Succeeded = false, StatusCode = 400 };
                }
            }
            else
            {
                return new Response<string> { Data = null, Message = $"Email {request.Email } is already registered.", ResponseCode = "-1", Succeeded = false, StatusCode = 400 };
            }
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("email", user.Email),
                new Claim("mcode", user.MerchantCode),
                new Claim("given_name", user.FirstName),
                new Claim("given_email", user.Email),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Auth/AddPassword/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
                await _userManager.AddPasswordAsync(user, password);
                return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public async Task<Response<string>> ForgotPassword(ForgotPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null)
            {
                return new Response<string>("", message: $"Reset email sent");
            }

            var resetLink = await GetResetUrl(account, model.WebUrl);

            var msgBody = EmailHelper.ResetPasswordMsg(account.Email, resetLink);

            try
            {
                //try to send email
                _emailService.SendEmail(account.Email, account.LastName, "Password Reset", msgBody);
            }
            catch (Exception ex)
            {
                // failed sending email

            }
            return new Response<string>(account.Id, message: $"Reset email sent");
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
            var result = await _userManager.ResetPasswordAsync(account, code, model.Password);
            if (result.Succeeded)
            {
                return new Response<string> { Data = "Password Resetted.", Message = $"Password Resetted.", ResponseCode = "00", Succeeded = true, StatusCode = 200 };
            }
            else
            {
                return new Response<string> { Data = null, Message = $"Password rest failed.", ResponseCode = "-1", Succeeded = false, StatusCode = 400 };
            }
        }

        public async Task<Response<string>> Logout()
        {
            await _signInManager.SignOutAsync();
            return new Response<string> { Data = "User logged out Successfully", Message = "User logged out Successfully", Succeeded = true };
        }

        public async Task<List<UserDTO>> GetUsers(string loggedInUserId)
        {
            var userloggedIn = await _userManager.FindByIdAsync(loggedInUserId);
            if (userloggedIn.IsGeneral)
            {

            }
            var users = await _userManager.Users.Where(c => c.MerchantCode == userloggedIn.MerchantCode && c.IsDeleted == false && c.IsGeneral == userloggedIn.IsGeneral).ToListAsync();
            var userss = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if ((await _userManager.IsInRoleAsync(user, "GlobalAdmin")))
                {

                }
                else
                {
                    userss.Add(user);
                }
            }
            var usersDto = userss.Where(c => c.Id != loggedInUserId).Select(c => new UserDTO
            {
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                UserId = c.Id,
                IsActive = c.IsActive,
                Status = c.IsActive ? "Active" : "In Active"
            }).ToList();
            foreach (var user in usersDto)
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.UserId));
                var role = await _roleManager.FindByNameAsync(roles.ToList().FirstOrDefault());
                user.Role = role.Name;
                user.RoleId = role.Id;
            }
            return usersDto;

        }
        public async Task<List<UserDTO>> GetUsers(string loggedInUserId, int count)
        {
            var userloggedIn = await _userManager.FindByIdAsync(loggedInUserId);
            //if (userloggedIn.IsGeneral)
            //{

            //}
            var users = await _userManager.Users.Where(c => c.MerchantCode == userloggedIn.MerchantCode && c.IsDeleted == false && c.IsGeneral == userloggedIn.IsGeneral).OrderByDescending(c => c.Id).Take(count).ToListAsync();
            var userss = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if ((await _userManager.IsInRoleAsync(user, "GlobalAdmin")))
                {

                }
                else
                {
                    userss.Add(user);
                }
            }
            var usersDto = userss.Where(c => c.Id != loggedInUserId).Select(c => new UserDTO
            {
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                UserId = c.Id,
                IsActive = c.IsActive,
                Status = c.IsActive ? "Active" : "In Active"
            }).ToList();
            foreach (var user in usersDto)
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.UserId));
                var role = await _roleManager.FindByNameAsync(roles.ToList().FirstOrDefault());
                user.Role = role.Name;
                user.RoleId = role.Id;
            }
            return usersDto;

        }
        public async Task<Response<string>> RemoveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new Response<string> { Data = "User not found", Message = "User not found", Succeeded = false };
            }
            user.IsDeleted = true;
            user.DeletedOn = DateTime.Now;
            user.DeletedBy = _authenticatedUserService.UserId;
            var response = await _userManager.UpdateAsync(user);
            if (response.Succeeded)
                return new Response<string> { Data = "User deleted successfully", Message = "User deleted successful", Succeeded = true };

            return new Response<string> { Data = "User not found", Message = "User not found", Succeeded = false };
        }

        public async Task<Response<List<RolesDTO>>> GetRoles()
        {
            var roles = new List<RolesDTO>();
            roles = await _roleManager.Roles.Where(c => c.Name != "GlobalAdmin" && c.Name != "StoreAdmin" && c.Name != "Customer" && c.Name != "BranchAdmin").Select(c => new RolesDTO
            {
                RoleId = c.Id,
                RoleName = c.Name,
            }).ToListAsync();
            //if (isGeneral)
            //{
            //    roles = await _roleManager.Roles.Where(c => c.Name != "GlobalAdmin" && c.Name != "StoreAdmin" && c.Name != "Customer" && c.Name != "BranchAdmin").Select(c => new RolesDTO
            //    {
            //        RoleId = c.Id,
            //        RoleName = c.Name,
            //    }).ToListAsync();
            //}
            //else
            //{
            //    roles = await _roleManager.Roles.Where(c => c.Name != "GlobalAdmin" && c.Name != "Maker" && c.Name != "Approver").Select(c => new RolesDTO
            //    {
            //        RoleId = c.Id,
            //        RoleName = c.Name,
            //    }).ToListAsync();
            //}

            return new Response<List<RolesDTO>> { Data = roles, Message = "roles retrived successfully", Succeeded = true };
        }

        public async Task<int> Enable(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsActive = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return 1;
            return 0;
        }

        public async Task<int> Disable(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return 1;
            return 0;
        }

        public async Task<int> ResendEmail(string email, string webUrl)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user.EmailConfirmed)
                {
                    return -2;
                }
                var verificationUrl = await SendVerificationEmail(user, webUrl);

                var msgBody = EmailHelper.ConfirmEmailTemplate(email, verificationUrl);
                _emailService.SendEmail(email, email, "RE- Account Confirmation", msgBody);
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        private async Task<string> GetResetUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Auth/ResetPassword/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "email", user.Email);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task UpdateUser(UpdateUser updateUserDto)
        {
            try
            {
                var userToBeUpdated = await _userManager.FindByIdAsync(updateUserDto.UserId);
                userToBeUpdated.FirstName = updateUserDto.FirstName;
                userToBeUpdated.LastName = updateUserDto.LastName;
                var result = await _userManager.UpdateAsync(userToBeUpdated);

                //This bloc checks if the role of user changed.
                var role = await _roleManager.FindByIdAsync(updateUserDto.RoleId);
                if (!await _userManager.IsInRoleAsync(userToBeUpdated, role.Name))
                {
                    await UpdateUserRole(userToBeUpdated.Id, updateUserDto.RoleId);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task UpdateUserRole(string userId, string roleId)
        {
            var newRole = await _roleManager.FindByIdAsync(roleId);
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            //removes a user from all existing roles
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            //adds the new role to the user.
            await _userManager.AddToRoleAsync(user, newRole.Name);

            //Delete all the page mapped to this user
            await DeletePagesAssignedToUser(userId);
        }

        private async Task DeletePagesAssignedToUser(string userId)
        {
            var pagesAssignedToUser = await _context.UsersRolePermission.Include(c => c.MenuSetup)
                .Where(c => c.UserId == userId)
                .Select(c => c.MenuSetup.MenuId)
                .ToListAsync();

            var result = await _menuRepository.DeleteMenuAssignedToUser(new MenuToUserRequest
            {
                Menus = pagesAssignedToUser,
                UserId = userId
            });
        }

        public async Task<int> GetUserCount(string loggedInUserId)
        {
            var userCount = await GetUsers(loggedInUserId);
            return userCount.Count();
        }
    }

}
