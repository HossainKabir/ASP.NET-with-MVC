using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EatmEntityFramework.Startup))]
namespace EatmEntityFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
