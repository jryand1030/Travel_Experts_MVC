using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Travel_Experts_MVC.Startup))]
namespace Travel_Experts_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
