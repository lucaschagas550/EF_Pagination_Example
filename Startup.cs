using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Business.Notifications;
using EF_Pagination_Example.Business.Services;
using EF_Pagination_Example.Business.Services.Admin;
using EF_Pagination_Example.Configuration;
using EF_Pagination_Example.Data;
using EF_Pagination_Example.Data.Repositories.DataAccess;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    options =>
                        {
                            options.EnableRetryOnFailure(
                                maxRetryCount: 3,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorNumbersToAdd: new List<int> { 4060 });
                        })
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

            services.AddIdentityConfig(Configuration);
            services.AddJwtConfig(Configuration);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            services.AddScoped<IInitialUserService, InitialUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, UsersManagementService>();
            services.AddScoped<IPermissionsManagementService, PermissionsManagementService>();
            
            services.AddScoped<ICategoryService, CategoryService>();
        }

        public static async Task Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var initialUser = scope.ServiceProvider.GetRequiredService<IInitialUserService>();
                await initialUser.CreateRole();
                await initialUser.CreateAdmin();
            }

            #region
            ///Verificar se existe alguma migracao pendente e aplicar todas ao iniciar a aplicacao
            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    dbContext.Database.Migrate();
            //}
            #endregion
        }
    }
}
