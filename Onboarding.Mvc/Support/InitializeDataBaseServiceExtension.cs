using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onboarding.Mvc.Data;
using Onboarding.Mvc.Models;
using Onboarding.Mvc.Services;

namespace Onboarding.Mvc.Support
{
    public static class InitializeDataBaseServiceExtension
    {
        public static async Task Initialize(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userMangerService = scope.ServiceProvider.GetRequiredService<UserManager<Hr>>();
                var chatService = scope.ServiceProvider.GetRequiredService<IChatsService>();

                if (dbService.Database.GetPendingMigrations().Any())
                    await dbService.Database.MigrateAsync();

                if (userMangerService.Users.Count() == 0)
                    await userMangerService.CreateAsync(new Hr() { Email = "1234@gmail.com", UserName = "1234@gmail.com" }, "1234");
            }
        }
    }
}
