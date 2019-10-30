using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shaheer_BugTracker.Startup))]
namespace Shaheer_BugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
