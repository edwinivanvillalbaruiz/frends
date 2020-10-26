using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirtualFrends.Startup))]
namespace VirtualFrends
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
