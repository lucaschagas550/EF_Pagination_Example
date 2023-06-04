using Microsoft.AspNetCore.Identity;
using EF_Pagination_Example.Data;
using EF_Pagination_Example.Model;

namespace EF_Pagination_Example.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services,
         IConfiguration configuration)
        {
            //regras do identiy
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddErrorDescriber<IdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            //JWT


            return services;
        }
    }
}
