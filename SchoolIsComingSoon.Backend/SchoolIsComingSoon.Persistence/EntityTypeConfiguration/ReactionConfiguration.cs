using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence.EntityTypeConfiguration
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.HasKey(reaction => reaction.Id);
            builder.HasIndex(reaction => reaction.Id).IsUnique();
            builder
                .HasOne(reaction => reaction.Post)
                .WithMany(post => post.Reactions)
                .HasForeignKey(reaction => reaction.PostId);
            builder
                .HasOne(reaction => reaction.AppUser)
                .WithMany(user => user.Reactions)
                .HasForeignKey(reaction => reaction.UserId);
        }
    }
}