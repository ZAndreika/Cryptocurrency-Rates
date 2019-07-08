using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptocurrencyRateWebApp.Models {
    public class RegisteringUser {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(4, ErrorMessage = "Password must have at least 4 characters")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}