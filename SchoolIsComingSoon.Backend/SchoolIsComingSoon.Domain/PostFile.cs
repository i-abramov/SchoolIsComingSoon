using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolIsComingSoon.Domain
{
    public class PostFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Base64Code { get; set; }
        public string FileType { get; set; }

        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}