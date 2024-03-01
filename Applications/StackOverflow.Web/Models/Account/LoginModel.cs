using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.Account
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email address!")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 6,
            ErrorMessage = "Password should be within 6 to 16 characters long. Use strong password.")]
        [DataType(DataType.Password, ErrorMessage = "Wrong Password!")]
        public string? Password { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
        public string? ReturnUrl { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
