using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Persistence
{
    public class DbInitializer
    {
        public static async void Initialize(SicsDbContext context)
        {
            if (context.Database.EnsureCreated())
            {
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