using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventWeb.Startup))]
namespace EventWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
