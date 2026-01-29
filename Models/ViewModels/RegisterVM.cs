using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(50, ErrorMessage ="Name cannot exceed 50 characters")]
        public required string Name { get; set; }
        [Required(ErrorMessage ="Role is required")]
        [MaxLength(20, ErrorMessage ="Role cannot exceed 20 characters")]
        public required string Role { get; set; }
        [Required(ErrorMessage ="password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$", 
            ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Phone(ErrorMessage = "Invalid mobile number format")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid mobile number format")]
        [MaxLength(10, ErrorMessage = "Mobile number cannot exceed 10 characters")]
        [MinLength(10, ErrorMessage = "Mobile number must be at least 10 characters")]
        public required string Mobile { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(10, ErrorMessage = "Username cannot exceed 10 characters")]
        public required string Username { get; set; }
    }
}