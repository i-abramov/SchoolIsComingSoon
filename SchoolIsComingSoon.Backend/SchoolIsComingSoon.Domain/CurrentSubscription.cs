using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolIsComingSoon.Domain
{
    public class CurrentSubscription
    {
        public Guid Id { get; set; }
        public DateTime ExpiresAfter { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public Guid SubscriptionId { get; set; }
        [ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }
    }
}