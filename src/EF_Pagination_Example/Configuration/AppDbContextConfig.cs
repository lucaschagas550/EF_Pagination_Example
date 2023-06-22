using EF_Pagination_Example.Data;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Configuration
{
    public static class AppDbContextConfig
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    options =>
                    {
                        options.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: new List<int> { 4060 });
                    })
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

            services.AddIdentityConfig(configuration);

            return services;
        }
    }
}
