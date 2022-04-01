using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Shopper.WebApi.Extensions
{
    public static class HealtCheckExtensions
    {
        public static IServiceCollection AddHealthChecksDashboard(this IServiceCollection services, string url)
        {
            services.AddHealthChecksUI()
                 .AddInMemoryStorage();

            return services;
        }

       
    }
}
