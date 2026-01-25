using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(50, ErrorMessage ="Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Role is required")]
        [MaxLength(20, ErrorMessage ="Role cannot exceed 20 characters")]
        public string Role { get; set; }
        [Required(ErrorMessage ="password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$", 
            ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }
    }
}