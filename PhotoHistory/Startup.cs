using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoHistory.Startup))]
namespace PhotoHistory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
