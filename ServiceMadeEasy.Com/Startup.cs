using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceMadeEasy.Com.Startup))]
namespace ServiceMadeEasy.Com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
