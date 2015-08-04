using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tweeter.Web.Startup))]
namespace Tweeter.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
