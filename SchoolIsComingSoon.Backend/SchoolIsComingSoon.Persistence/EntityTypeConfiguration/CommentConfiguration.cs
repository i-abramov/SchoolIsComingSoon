using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence.EntityTypeConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(comment => comment.Id);
            builder.HasIndex(comment => comment.Id).IsUnique();
            builder
                .HasOne(comment => comment.Post)
                .WithMany(post => post.Comments)
                .HasForeignKey(comment => comment.PostId);
            builder
                .HasOne(comment => comment.AppUser)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId);
        }
    }
}