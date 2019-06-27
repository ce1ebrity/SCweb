using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCWeb.Startup))]
namespace SCWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
