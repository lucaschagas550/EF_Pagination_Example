using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Data;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Configuration
{
    public static class EntityFrameworkConfig
    {
        public static async Task UseEntityFramework(this WebApplication app)
        {
            using var initialUserScope = app.Services.CreateScope();
            var initialUser = initialUserScope.ServiceProvider.GetRequiredService<IInitialUserService>();
            await initialUser.CreateRole();
            await initialUser.CreateAdmin();

            using var applyMigrationScope = app.Services.CreateScope();
            var dbContext = applyMigrationScope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync().ConfigureAwait(false);
        }
    }
}