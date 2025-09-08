using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;
using SchoolIsComingSoon.Persistence.EntityTypeConfiguration;

namespace SchoolIsComingSoon.Persistence
{
    public class SicsDbContext : DbContext, ISicsDbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<PostFile> Files { get; set; }
        public DbSet<PostImage> Images { get; set; }
        public DbSet<PostCategory> Categories { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<CurrentSubscription> CurrentSubscriptions { get; set; }

        public SicsDbContext(DbContextOptions<SicsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new SubscriptionConfiguration());
            builder.ApplyConfiguration(new CurrentSubscriptionConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new PostFileConfiguration());
            builder.ApplyConfiguration(new PostImageConfiguration());
            builder.ApplyConfiguration(new PostCategoryConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new ReactionConfiguration());
            base.OnModelCreating(builder);
        }
    }
}