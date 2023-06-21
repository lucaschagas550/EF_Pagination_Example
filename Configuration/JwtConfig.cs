using EF_Pagination_Example.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EF_Pagination_Example.Configuration
{
    public static class TokenConfig
    {
        public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Token");
            services.Configure<Token>(appSettingsSection);

            var token = appSettingsSection.Get<Token>();
            if (token == null)
                return services;

            var key = Encoding.ASCII.GetBytes(token.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = token.ValidOn,
                    ValidIssuer = token.Issuer
                };
            });

            return services;
        }

        public static void UseAuthConfiguration(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}