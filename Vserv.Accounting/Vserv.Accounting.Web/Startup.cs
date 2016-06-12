using Microsoft.Owin;
using Owin;
using Vserv.Accounting.Web;

[assembly: OwinStartup(typeof(Startup))]
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
