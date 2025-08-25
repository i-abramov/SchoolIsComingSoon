namespace SchoolIsComingSoon.Domain
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Reaction> Reactions { get; set; } = new();
    }
}