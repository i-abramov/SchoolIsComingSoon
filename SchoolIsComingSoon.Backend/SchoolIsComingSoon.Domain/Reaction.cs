using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolIsComingSoon.Domain
{
    public class Reaction
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}