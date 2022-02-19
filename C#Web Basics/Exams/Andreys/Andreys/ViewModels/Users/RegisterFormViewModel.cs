using System.ComponentModel.DataAnnotations;

namespace Andreys.ViewModels.Users
{
    public class RegisterFormViewModel
    {
        [StringLength(10, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string username { get; set; }
        [EmailAddress(ErrorMessage = "Email must be valid email")]
        public string email { get; set; }
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string password { get; set; }
        [Compare(nameof(password), ErrorMessage = "Password and ConfirmPassword must be equal")]
        public string confirmPassword { get; set; }
    }
}
