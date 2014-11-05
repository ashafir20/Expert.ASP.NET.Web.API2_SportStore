using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace SportStore.Infrastructure.Identity
{
    public class StoreRoleManager : RoleManager<StoreRole>
    {
        public StoreRoleManager(RoleStore<StoreRole> store) : base(store) { }

        public static StoreRoleManager Create(
            IdentityFactoryOptions<StoreRoleManager> options,
            IOwinContext context)
        {
           StoreIdentityDbContext dbContext = context.Get<StoreIdentityDbContext>();
           StoreRoleManager manager =
               new StoreRoleManager(new RoleStore<StoreRole>(dbContext));

           return manager;
        }
    }
}