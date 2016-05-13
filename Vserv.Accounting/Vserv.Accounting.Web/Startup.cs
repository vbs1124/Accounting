using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vserv.Accounting.Web.Startup))]
namespace Vserv.Accounting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
