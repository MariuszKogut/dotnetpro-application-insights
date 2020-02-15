using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HS.CustomerApp.HostConfiguration
{
    public static class ApplicationInsightsExtensions
    {
        public static void AddCustomizedApplicationInsightsTelemetry(this IServiceCollection services,
            IConfiguration configuration, string roleName, string roleNameInstance)
        {
            var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            if (string.IsNullOrEmpty(instrumentationKey))
            {
                throw new ArgumentNullException(nameof(instrumentationKey));
            }

            services.AddApplicationInsightsTelemetry(instrumentationKey);

            services.AddSingleton<ITelemetryInitializer>(new RoleNameInitializer(roleName, roleNameInstance));
        }
    }
}