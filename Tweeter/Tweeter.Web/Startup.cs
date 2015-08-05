using Microsoft.Owin;
using Tweeter.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Tweeter.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
