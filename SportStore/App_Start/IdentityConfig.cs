using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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
        }
    }
}