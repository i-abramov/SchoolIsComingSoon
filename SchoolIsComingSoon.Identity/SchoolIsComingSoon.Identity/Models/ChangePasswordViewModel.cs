using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.Identity.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}