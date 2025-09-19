using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence
{
    public class DbInitializer
    {
        public static async void Initialize(SicsDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                await context.Subscriptions.AddAsync(
                    new Subscription()
                    {
                        Name = "Бесплатная",
                        Price = 0,
                    });

                await context.Subscriptions.AddAsync(
                    new Subscription()
                    {
                        Name = "Базовая",
                        Price = 100,
                    });

                await context.Subscriptions.AddAsync(
                    new Subscription()
                    {
                        Name = "Продвинутая",
                        Price = 250,
                    });

                await context.Subscriptions.AddAsync(
                    new Subscription()
                    {
                        Name = "Максимальная",
                        Price = 380,
                    });

                await context.Categories.AddRangeAsync(
                    new PostCategory()
                    {
                        Name = "Дошкольное образование"
                    },
                    new PostCategory()
                    {
                        Name = "Начальные классы"
                    },
                    new PostCategory()
                    {
                        Name = "Информация для родителей"
                    });

                await context.SaveChangesAsync();
            }
        }
    }
}