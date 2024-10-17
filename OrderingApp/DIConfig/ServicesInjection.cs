using Microsoft.Fast.Components.FluentUI.Components.Tooltip;
using OrderingApp.Logic.Services;
using OrderingApp.Logic.Services.Interface;
using System.Reflection;

namespace OrderingApp.DIConfig
{
    public static class ServicesInjection
    {

        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            var assemblies = Assembly.Load("OrderingApp.Logic");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));

            services.AddAutoMapper(assemblies);

            services.AddScoped<ITooltipService, TooltipService>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IValueComputationService, ValueComputationService>();


            return services;
        }
    }
}
