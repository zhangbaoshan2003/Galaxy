using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Galaxy.Startup))]
namespace Galaxy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
