using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PureLoginMVC.Startup))]
namespace PureLoginMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
