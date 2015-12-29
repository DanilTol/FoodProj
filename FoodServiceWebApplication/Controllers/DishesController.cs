using System.Linq;
using System.Net;
using System.Web.Mvc;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;

namespace FoodServiceWebApplication.Controllers
{
    public class DishesController : Controller
    {
        private readonly IDishService _dishClass;

        public DishesController(IDishService dishClass)
        {
            _dishClass = dishClass;
        }

        private const string DefaultPathToImage = "../../Dish/Common.gif";


        // GET: Dishes
        public ActionResult Index()
        {
            var re = _dishClass.GetAllDishes().ToList();
            return View(re);
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detailsInfo = _dishClass.GetDishById((int)id);
            if (detailsInfo == null)
            {
                return HttpNotFound();
            }
           
            return View(detailsInfo);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Ingridients,Energy,Weight,Price,Description")] DishModelDetailsInfo dish,[Bind(Include = "Path")] string[] pathStrings)
        {
            try
            {
                _dishClass.CreateDish(dish);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DishModelDetailsInfo detailsInfo = _dishClass.GetDishById((int)id);
            if (detailsInfo == null)
            {
                return HttpNotFound();
            }
            return View(detailsInfo);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Ingridients,Energy,Weight,Price,Description")] DishModelDetailsInfo dish)
        {
            try
            {
                _dishClass.EditDish(dish);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DishModelDetailsInfo detailsInfo = _dishClass.GetDishById((int)id);
            if (detailsInfo == null)
            {
                return HttpNotFound();
            }
            return View(detailsInfo);
        }

        //// POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _dishClass.DeleteDish(id);
            return RedirectToAction("Index");
        }
    }
}
