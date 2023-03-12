using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Business.Notifications;
using EF_Pagination_Example.Business.Services;
using EF_Pagination_Example.Data;
using EF_Pagination_Example.Data.Repositories.DataAccess;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow;
using EF_Pagination_Example.Data.Uow.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICategoryServices, CategoryService>();
            services.AddScoped<INotifier, Notifier>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
