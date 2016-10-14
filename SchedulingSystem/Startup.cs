using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchedulingSystem.Startup))]
namespace SchedulingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
