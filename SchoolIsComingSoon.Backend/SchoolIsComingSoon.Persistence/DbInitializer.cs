using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence
{
    public class DbInitializer
    {
        public static async void Initialize(SicsDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
                await context.Subscriptions.AddRangeAsync(
                    new Subscription()
                    {
                        Name = "Бесплатная",
                        Price = 0,
                    },
                    new Subscription()
                    {
                        Name = "Базовая",
                        Price = 100,
                    },
                    new Subscription()
                    {
                        Name = "Продвинутая",
                        Price = 250,
                    },
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