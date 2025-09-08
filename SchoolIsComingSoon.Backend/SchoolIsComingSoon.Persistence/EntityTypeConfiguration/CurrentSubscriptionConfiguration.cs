using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence.EntityTypeConfiguration
{
    public class CurrentSubscriptionConfiguration : IEntityTypeConfiguration<CurrentSubscription>
    {
        public void Configure(EntityTypeBuilder<CurrentSubscription> builder)
        {
            builder.HasKey(subscription => subscription.Id);
            builder.HasIndex(subscription => subscription.Id).IsUnique();
            builder
                .HasOne(subscription => subscription.Subscription)
                .WithMany(user => user.CurrentSubscriptions)
                .HasForeignKey(subscription => subscription.SubscriptionId);
            builder
                .HasOne(subscription => subscription.User)
                .WithOne(user => user.Subscription)
                .HasForeignKey<CurrentSubscription>(subscription => subscription.UserId);
        }
    }
}