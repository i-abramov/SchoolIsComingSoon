using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolIsComingSoon.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string CreationDate { get; set; }
        public string? EditDate { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        public List<Comment> Comments { get; set; } = new();
        public List<Reaction> Reactions { get; set; } = new();
        public List<PostImage> Images { get; set; } = new();
        public List<PostFile> Files { get; set; } = new();
        public List<PostCategory> Categories { get; set; } = new();
    }
}