using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using SecuredSwashbuckleExample.Providers;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(SecuredSwashbuckleExample.Startup))]
namespace SecuredSwashbuckleExample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            var thisAssembly = typeof(SecuredSwashbuckleExample.Startup).Assembly;
            config
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "A title for your API");
                        c.DocumentFilter<AuthTokenDocFilter>();
                    })
                .EnableSwaggerUi(c => c.InjectJavaScript(thisAssembly, "SecuredSwashbuckleExample.CustomContent.swashbuckle-custom-auth.js"));  
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}