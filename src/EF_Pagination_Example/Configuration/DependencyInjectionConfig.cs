﻿using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Business.Notifications;
using EF_Pagination_Example.Business.Services.Admin;
using EF_Pagination_Example.Business.Services;
using EF_Pagination_Example.Data.Repositories.DataAccess;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Data.Uow;
using EF_Pagination_Example.Data;
using EF_Pagination_Example.Extensions;

namespace EF_Pagination_Example.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IInitialUserService, InitialUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, UsersManagementService>();
            services.AddScoped<IPermissionsManagementService, PermissionsManagementService>();

            services.AddScoped<ICategoryService, CategoryService>(serviceProvider => serviceProvider.CategoryService());
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryProductService, CategoryProductService>();
            services.AddScoped<ISupplierServices, SupplierServices>();
        }

    }
}

//Pode ser uma outra classe
public static class ConfigureServices
{
    //Metodo para realizar uma configuracao especifica para o CategoryService, podendo receber valores de um vault por exemplo e injetar no service como um REDIS_PORT
    public static CategoryService CategoryService(this IServiceProvider service)
    {
        var categoryRepository = service.GetRequiredService<ICategoryRepository>();
        var notifier = service.GetRequiredService<INotifier>();

        return new CategoryService(notifier, categoryRepository);
    }
}
