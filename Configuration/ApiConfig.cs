using EF_Pagination_Example.Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            var urlClients = new[]{ "",};

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                            .AllowAnyOrigin() //urlClients => WithOrigings
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
        }

        public static async Task UseApiConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseCors("CorsPolicy");

            app.UseAuthConfiguration();

            using var scope = app.Services.CreateScope();
            var initialUser = scope.ServiceProvider.GetRequiredService<IInitialUserService>();
            await initialUser.CreateRole();
            await initialUser.CreateAdmin();
        }
    }
}
