using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence.EntityTypeConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(post => post.Id);
            builder.HasIndex(post => post.Id).IsUnique();
            builder
                .HasOne(post => post.AppUser)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.UserId);
            builder
                .HasMany(post => post.Categories)
                .WithMany(category => category.Posts);
        }
    }
}