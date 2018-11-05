using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Sveit.AppServices.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sveit.AppServices
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Ativar o método para gerar o OAuth Token
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(92),
                AllowInsecureHttp = true
            });
        }
    }
}