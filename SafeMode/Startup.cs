using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SafeMode.Startup))]
namespace SafeMode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
