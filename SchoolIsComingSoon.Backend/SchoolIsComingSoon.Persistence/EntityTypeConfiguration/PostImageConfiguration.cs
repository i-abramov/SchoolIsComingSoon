using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence.EntityTypeConfiguration
{
    public class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.HasKey(image => image.Id);
            builder.HasIndex(image => image.Id).IsUnique();
            builder
                .HasOne(image => image.Post)
                .WithMany(post => post.Images)
                .HasForeignKey(image => image.PostId);
        }
    }
}