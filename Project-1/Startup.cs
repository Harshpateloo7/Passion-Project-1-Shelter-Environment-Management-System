using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_1.Startup))]
namespace Project_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
