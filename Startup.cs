using EF_Pagination_Example.Configuration;

namespace EF_Pagination_Example
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddDbContextConfig(Configuration);

            services.AddJwtConfig(Configuration);

            services.AddSwaggerConfiguration();

            services.RegisterServices();
        }

        public static async Task Configure(WebApplication app)
        {
            app.UseSwaggerConfiguration();

            await app.UseApiConfiguration().ConfigureAwait(false);

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
