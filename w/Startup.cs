using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(w.Startup))]
namespace w
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
