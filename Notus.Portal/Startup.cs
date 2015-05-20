using Microsoft.Owin;
using Notus.Portal;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace Notus.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}