using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobWindowNew.Web.Startup))]
namespace JobWindowNew.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
