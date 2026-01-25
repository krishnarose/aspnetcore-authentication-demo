using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models.ViewModels
{
    public class LoginVM
    {
        [Required (ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string Username { get; set; }
        [Required (ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$", 
            ErrorMessage = "Unauthenticate User.")]
        public string Password { get; set; }
    }
}