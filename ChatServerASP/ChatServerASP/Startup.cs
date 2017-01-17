using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
//using ChatServerASP.Providers;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;


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


/*
[assembly: OwinStartup(typeof(ChatServerASP.Startup))]

namespace ChatServerASP
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new SimpleAuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }

    }
}
*/

