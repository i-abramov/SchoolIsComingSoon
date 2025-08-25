namespace SchoolIsComingSoon.Domain
{
    public class PostCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}