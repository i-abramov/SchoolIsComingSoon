using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SchoolIsComingSoon.Persistence
{
    public class SicsDbContextFactory : IDesignTimeDbContextFactory<SicsDbContext>
    {
        public SicsDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("DbConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Environment variable DbConnection is not set.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<SicsDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new SicsDbContext(optionsBuilder.Options);
        }
    }
}