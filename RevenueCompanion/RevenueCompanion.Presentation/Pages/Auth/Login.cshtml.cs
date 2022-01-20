using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.DTOs.Account;
using RevenueCompanion.Domain.Entities;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.DTOs.Authentication;
using RevenueCompanion.Presentation.DTOs.Base;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        private readonly MerchantConfiguration _merchantConfig;
        public LoginModel(IHttpClientService httpClientService,
                          IOptions<ApiConfiguration> settings,
                          IOptions<MerchantConfiguration> merchantConfig)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
            _merchantConfig = merchantConfig.Value;
        }
        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        public bool RememberMe { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ViewData["Action"] = "Login";
            var siteLocation = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var merchantConfig = await GetMerchantConfig(siteLocation);
            if (merchantConfig != null)
            {
                if (merchantConfig.Color != null)
                {
                    HttpContext.Session.SetString("color", merchantConfig.Color);
                    HttpContext.Session.SetString("logo", merchantConfig.Logo);
                }
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var loginRequest = new LoginRequestDTO { email = this.Email, password = this.Password };

            var response = await _httpClientService.PostAsync<LoginRequestDTO, BaseDTO<AuthenticationResponse>>($"{_settings.BaseUrl}{ApplicationContants.Authenticate}", loginRequest);

            if (!response.succeeded)
            {
                ModelState.AddModelError("", response.message);
                return Page();
            }
            //var claims = new List<Claim>();

            //claims.Add(new Claim(ClaimTypes.Name, response.data.userName));
            //var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var claimPrincipal = new ClaimsPrincipal(claimIdenties);
            //var authenticationManager = Request.HttpContext;

            //// Sign In.  
            //await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties() { });

            // HttpContext.Session.SetComplexData("loggedInUserDetails", response.data);
            var siteLocation = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            HttpContext.Session.SetString("jwtToken", response.data.JWToken);
            HttpContext.Session.SetString("userId", response.data.Id);
            HttpContext.Session.SetString("role", response.data.Roles.FirstOrDefault());
            HttpContext.Session.SetString("FullName", response.data.FullName != null ? response.data.FullName : "I dont have a name");
            HttpContext.Session.SetString("BaseUrl", _settings.BaseUrl);
            HttpContext.Session.SetString("siteLocation", siteLocation);
            HttpContext.Session.SetString("merchantCode", response.data.MerchantCode);

            if (response.data.Roles.Select(c => c.ToLower()).Contains("globaladmin"))
            {
                return RedirectToPage("/Dashboard/Global");
            }
            else
            {
                return RedirectToPage("/Dashboard/User");
            }

        }

        public async Task<MerchantConfig> GetMerchantConfig(string browserUrl)
        {
            var merchantConfig = await _httpClientService.GetAsync<MerchantConfig>($"{_settings.BaseUrl}{ApplicationContants.GetStateUrlSetup}", "", browserUrl);
            return merchantConfig;
        }
    }
}
