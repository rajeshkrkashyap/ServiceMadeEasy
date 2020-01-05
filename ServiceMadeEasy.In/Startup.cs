using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceMadeEasy.In.Startup))]
namespace ServiceMadeEasy.In
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
