using System.Reflection;

namespace OrderingApp.Restaurant.Api.DIConfig
{
    public static class ServicesInjection
    {

        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            var assemblies = Assembly.Load("OrderingApp.Restaurant.Logic");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));

            services.AddAutoMapper(assemblies);

            return services;
        }
    }
}
