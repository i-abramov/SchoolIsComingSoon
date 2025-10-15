using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolIsComingSoon.Identity.Data;

namespace SchoolIsComingSoon.Identity
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("DbConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Environment variable DbConnection is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}