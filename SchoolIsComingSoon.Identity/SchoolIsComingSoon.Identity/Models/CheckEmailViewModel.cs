using System.ComponentModel.DataAnnotations;

namespace SchoolIsComingSoon.Identity.Models
{
    public class CheckEmailViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}