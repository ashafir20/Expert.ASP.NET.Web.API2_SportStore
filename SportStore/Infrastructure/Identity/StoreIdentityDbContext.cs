using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportStore.Infrastructure.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<StoreUser>
    {
        public StoreIdentityDbContext()
            : base("name=SportsStoreIdentityDb")
        {
            Database.SetInitializer<StoreIdentityDbContext>(new StoreIdentityDbInitializer());
        }

        public static StoreIdentityDbContext Create()
        {
            return new StoreIdentityDbContext();
        }
    }
}