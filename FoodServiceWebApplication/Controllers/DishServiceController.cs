using System.Linq;
using System.Net;
using System.Web.Mvc;
using FoodService.Business.DTO;
using FoodService.Business.Services;

namespace FoodServiceWebApplication.Controllers
{
    public class DishServiceController : Controller
    {
        //public DishesController(): this(new DishClassCall(new En), new )
        //{

        //}

        //public DishesController(IDishClassCall dishClass)
        //{
        //    this.dishClass = dishClass;
        //}
        
        private readonly DishService _dishClass;

        public DishServiceController(DishService dishClass)
        {
            _dishClass = dishClass;
        }

        private const string DefaultPathToImage = "../../Dish/Common.gif";


        // GET: Dishes
        public ActionResult Index()
        {
            var re = _dishClass.GetAllDishes().ToList();
            //re.Where(r => string.IsNullOrEmpty(r.ImagePath)).ToList().ForEach(rr => rr.ImagePath = "../Dish/Common.gif");
            foreach (var item in re)
            {
                if (string.IsNullOrEmpty(item.ImagePath))
                {
                    item.ImagePath = DefaultPathToImage;
                }
            }
            return View(re);
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
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
            //var z = new string[0];
                if (detailsInfo.ImagePath.Length < 1)
                    detailsInfo.ImagePath = new []{DefaultPathToImage};

            return View(detailsInfo);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            //DishModelDetailsInfo detailsInfo = dishClass.GetDishById((int)id);
            _dishClass.DeleteDish(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        dishClass.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
