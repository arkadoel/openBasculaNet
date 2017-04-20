using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(openBasculaNetWeb.Startup))]
namespace openBasculaNetWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DisableApplicationInsightsOnDebug();
        }

        /// <summary>
        /// Disables the application insights locally.
        /// </summary>
        [Conditional("DEBUG")]
        private static void DisableApplicationInsightsOnDebug()
        {
            TelemetryConfiguration.Active.DisableTelemetry = true;
        }
    }
}
