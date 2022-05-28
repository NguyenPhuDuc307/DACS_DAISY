using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAISY.Startup))]
namespace DAISY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
