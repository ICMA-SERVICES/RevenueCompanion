using Microsoft.AspNetCore.Http;
using RevenueCompanion.Application.Interfaces;
using System.Security.Claims;

namespace RevenueCompanion.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            MerchantCode = httpContextAccessor.HttpContext?.User?.FindFirstValue("mcode");
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue("given_email");
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue("given_name");
        }

        public string UserId { get; }

        public string MerchantCode { get; }
        public string Email { get; }
        public string Name { get; }
    }
}
