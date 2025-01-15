using Accounting.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Accounting.MigrationApplyer
{
    public static class MigrationApplyer
    {
        public static void ApplyMigrations(IServiceProvider provider)
        {
            try
            {
                using var scope = provider.CreateScope();
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while migrating: {ex.Message}");
            }
        }
    }
}
