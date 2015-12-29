using System.Web.Mvc;

namespace FoodService.WebApi2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
