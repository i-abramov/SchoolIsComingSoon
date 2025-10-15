using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolIsComingSoon.Application.Interfaces;

namespace SchoolIsComingSoon.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];

            services.AddDbContext<SicsDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<ISicsDbContext>(provider => provider.GetRequiredService<SicsDbContext>());

            return services;
        }
    }
}