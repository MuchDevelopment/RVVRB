using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rvvrb3.Startup))]
namespace rvvrb3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
