using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolIsComingSoon.Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string? EditDate { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}