using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SportStore.Infrastructure.Identity;

[assembly: OwinStartup(typeof(SportStore.IdentityConfig))]
namespace SportStore
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<StoreIdentityDbContext>(StoreIdentityDbContext.Create);
            app.CreatePerOwinContext<StoreUserManager>(StoreUserManager.Create);
            app.CreatePerOwinContext<StoreRoleManager>(StoreRoleManager.Create);

        /*  I configured ASP.NET Identity so that it will set a cookie when a request is successfully authenticated, which allows
            subsequent requests from the same client to be authorized without needing credentials. The cookie is required for
            round-trip applications, and the authentication test I have used in this chapter will not work without it. I will be using
            a different approach for the SportsStore application, which is to explicitly set an HTTP header to provide proof that
            the client has been authenticated. I explain this process in Chapter 6, but my final step in this chapter is to disable the
            cookie, as shown in Listing 5-22.*/

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            //});

            /*Page 113
             Listing 6-8 shows the statements that I added to configure Identity to use the provider class I created in the previous
             section and to set up authentication as part of the Web API request handling process.
             I have set three configuration properties that control the way that requests are authenticated. The Provider
             property specifies the object that will authenticate the user, which is in this case an instance of the StoreAuthProvider
             class that I defined in Listing 6-7.*/
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                Provider = new StoreAuthProvider(),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authenticate")
            });


/*          Setting the AllowInsecureHttp property to true allows authentication to be performed for any HTTP request
            rather than the default behavior, which is to support only SSL requests.
            The final property—TokenEndpointPath—specifies a URL that will be used to receive and process authentication
            requests. I have specified /Authenticate, which means that clients will send their authentication requests to
            http://localhost:6100/authenticate, as I demonstrate in the next section.*/

/*          Tip There are many configuration options for authentication—too many for me to describe in this book. See
            http://msdn.microsoft.com/en-us/library/microsoft.owin.security.oauth.oauthauthorizationserveroptions(v=vs.113).aspx for the full list.*/

/*          ASP.NET Identity can also be configured to use cookies for authentication, which means you don’t need to set the
            Authorization header. I am using the header approach because it lets me have more control over the authentication
            process for the SportsStore application, as you will see in Chapter 6, which makes demonstrating the functionality
            simpler. I disabled the cookie support in Chapter 5, but you can leave it enabled it in your own applications. I also
            demonstrate the cookie-based approach in Pro ASP.NET MVC 5 Platform, which is published by Apress.*/
        }
    }
}