using System.Threading.Tasks;
using System.Web.Mvc;
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

        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _repo.DeleteProductAsync(id);
            return RedirectToAction("index");
        }

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
    }
}