using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace HS.CustomerApp.HostConfiguration
{
    public class RoleNameInitializer : ITelemetryInitializer
    {
        private readonly string _roleName;
        private readonly string _roleInstance;

        public RoleNameInitializer(string roleName, string roleInstance)
        {
            _roleName = roleName;
            _roleInstance = roleInstance;
        }
        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                telemetry.Context.Cloud.RoleName = _roleName;
                telemetry.Context.Cloud.RoleInstance = _roleInstance;
            }
        }
    }
}