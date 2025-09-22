namespace SchoolIsComingSoon.Domain
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int LVL { get; set; }

        public List<CurrentSubscription> CurrentSubscriptions { get; set; } = new();
        public List<Post> Posts { get; set; } = new();
    }
}