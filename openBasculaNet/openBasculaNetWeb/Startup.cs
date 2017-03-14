using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(openBasculaNetWeb.Startup))]
namespace openBasculaNetWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
