using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.Identity.Models
{
    public class RecoverPasswordViewModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmNewPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}