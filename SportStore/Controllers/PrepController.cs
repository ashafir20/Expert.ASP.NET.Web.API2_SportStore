using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SportStore.Infrastructure.Identity;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class PrepController : Controller
    {
        private IRepository _repo;

        public PrepController()
        {
            _repo = new ProductRepository();
        }

        public ActionResult Index()
        {
            return View(_repo.Products);
        }

        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _repo.DeleteProductAsync(id);
            return RedirectToAction("index");
        }

        [Authorize(Roles = "Administrators")]
        public async Task<ActionResult> SaveProduct(Product product)
        {
            await _repo.SaveProductAsync(product);
            return RedirectToAction("Index");
        }

        public ActionResult Orders()
        {
            return View(_repo.Orders);
        }

        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _repo.DeleteOrderAsync(id);
            return RedirectToAction("index");
        }

        public async Task<ActionResult> SaveOrder(Order order)
        {
            await _repo.SaveOrderAsync(order);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SignIn()
        {
            IAuthenticationManager authMgr = HttpContext.GetOwinContext().Authentication;
            StoreUserManager userMgr = HttpContext.GetOwinContext().GetUserManager<StoreUserManager>();

            StoreUser user = await userMgr.FindAsync("Admin", "secret");
            authMgr.SignIn(await userMgr.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
            return RedirectToAction("Index");
        }

        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}