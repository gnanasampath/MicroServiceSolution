using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder, bool isProduction)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        private static void SeedData(AppDbContext appDbContext, bool isProduction)
        {

            if (isProduction)
            {
                Console.WriteLine("Applying migration");
                try
                {
                    appDbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not do migration{ex.Message}");
                }
            }
            if (!appDbContext.Platforms.Any())
            {
                Console.WriteLine("Seeding Data");
                appDbContext.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native computing foundation", Cost = "Free" });
                appDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Data already loaded");
            }
        }
    }
}
