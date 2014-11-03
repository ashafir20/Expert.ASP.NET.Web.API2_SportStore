using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SportStore.IdentityConfig))]
namespace SportStore
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app) { }
    }
}