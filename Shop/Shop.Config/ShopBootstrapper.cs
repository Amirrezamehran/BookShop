using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Products.Create;
using Shop.Application.Roles.Create;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAggregate.Services;
using Shop.Domain.ProductAggregate.Services;
using Shop.Domain.SellerAggregate.Services;
using Shop.Domain.UserAggregate.Services;
using Shop.Infrastructure;
using Shop.Presentation.Facade;
using Shop.Query.Categories.GetById;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
        {
        
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Directories).Assembly);
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetCategoryByIdQuery).Assembly);
            });

            //services.AddMediatR(typeof(Directories).Assembly);
            //services.AddMediatR(typeof(GetCategoryByIdQuery).Assembly);

            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient<IProductDomainService, ProductDomainService>();
            services.AddTransient<ISellerDomainService, SellerDomainService>();
            services.AddTransient<IUserDomainService, UserDomainService>();

            services.AddValidatorsFromAssembly(typeof(CreateRoleCommandValidator).Assembly);


            InfrastructureBootstrapper.Init(services, connectionString);
            services.InitFacadeDependency();

        }

    } // End Class

}





