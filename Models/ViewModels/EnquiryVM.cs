using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models.ViewModels
{
    public class EnquiryVM
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Last Name is requiresd.")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
