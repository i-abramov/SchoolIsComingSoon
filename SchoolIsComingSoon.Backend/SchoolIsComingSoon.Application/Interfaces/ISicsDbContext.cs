using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.Interfaces
{
    public interface ISicsDbContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Reaction> Reactions { get; set; }
        DbSet<PostFile> Files { get; set; }
        DbSet<PostImage> Images { get; set; }
        DbSet<PostCategory> Categories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
