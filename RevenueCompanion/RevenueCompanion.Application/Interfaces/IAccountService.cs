using RevenueCompanion.Application.DTOs.Account;
using RevenueCompanion.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code, string password);
        Task<Response<string>> ForgotPassword(ForgotPasswordRequest model);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<Response<string>> Logout();
        Task<int> ResendEmail(string email, string webUrl);
        Task<int> GetUserCount(string loggedInUserId);
        Task<List<UserDTO>> GetUsers(string loggedInUserId);
        Task<List<UserDTO>> GetUsers(string loggedInUserId, int count);
        Task<Response<string>> RemoveUser(string userId);

        Task<Response<List<RolesDTO>>> GetRoles();

        Task<int> Enable(string userId);
        Task<int> Disable(string userId);
        Task UpdateUser(UpdateUser updateUserDto);
    }
}
