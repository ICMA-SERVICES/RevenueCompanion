using System.ComponentModel.DataAnnotations;

namespace RevenueCompanion.Application.DTOs.Account
{
    public class RegisterRequest
    {
        public string MerchantCode { get; set; }
        public string RoleId { get; set; }
        public string WebUrl { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsWithRoleName { get; set; }

    }
}
