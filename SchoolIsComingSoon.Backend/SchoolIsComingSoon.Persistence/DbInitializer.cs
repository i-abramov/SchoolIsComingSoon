using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(SicsDbContext context)
        {
            if (context.Subscriptions.Any() || context.Categories.Any())
            {
                return;
            }

            await context.Subscriptions.AddRangeAsync(
                new Subscription { Name = "Бесплатная", Price = 0, LVL = 0 },
                new Subscription { Name = "Базовая", Price = 100, LVL = 1 },
                new Subscription { Name = "Продвинутая", Price = 250, LVL = 2 },
                new Subscription { Name = "Максимальная", Price = 380, LVL = 3 }
            );

            await context.Categories.AddRangeAsync(
                new PostCategory { Name = "Дошкольное образование" },
                new PostCategory { Name = "Начальные классы" },
                new PostCategory { Name = "Информация для родителей" }
            );

            await context.SaveChangesAsync();
        }
    }
}
