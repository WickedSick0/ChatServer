using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatServerASP.Startup))]
namespace ChatServerASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
