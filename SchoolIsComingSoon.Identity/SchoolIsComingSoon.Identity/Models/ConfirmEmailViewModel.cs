using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.Identity.Models
{
    public class ConfirmEmailViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }
    }
}