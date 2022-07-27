using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proms.Startup))]
namespace Proms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
