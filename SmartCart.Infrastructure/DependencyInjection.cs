using Microsoft.Extensions.DependencyInjection;
using SmartCart.Application.Contracts;
using SmartCart.Infrastructure.Services;

namespace SmartCart.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
