using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence.EntityTypeConfiguration
{
    public class PostFileConfiguration : IEntityTypeConfiguration<PostFile>
    {
        public void Configure(EntityTypeBuilder<PostFile> builder)
        {
            builder.HasKey(file => file.Id);
            builder.HasIndex(file => file.Id).IsUnique();
            builder
                .HasOne(file => file.Post)
                .WithMany(post => post.Files)
                .HasForeignKey(file => file.PostId);
        }
    }
}