using Microsoft.Owin;
using Notus;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace Notus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}