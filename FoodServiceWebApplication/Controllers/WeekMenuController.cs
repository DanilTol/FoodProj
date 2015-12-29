using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodServiceWebApplication.Attributes;

namespace FoodServiceWebApplication.Controllers
{
    [MyAuthorize("admin")]
    public class WeekMenuController : Controller
    {
        private readonly IDaySetService _daySetService;
        private readonly IDishService _dishService;

        public WeekMenuController(IDaySetService daySetService, IDishService dishService)
        {
            _daySetService = daySetService;
            _dishService = dishService;
        }


        //WeekDishSetClassCall weekDish = new WeekDishSetClassCall();
        private const string DefaultPathToImage = "../../Dish/Common.gif";
        //private DishService dishClass = new DishService();


        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class MultipleButtonAttribute : ActionNameSelectorAttribute
        {
            public string Name { get; set; }
            public string Argument { get; set; }

            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                var isValidName = false;
                var keyValue = string.Format("{0}:{1}", Name, Argument);
                var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);

                if (value != null)
                {
                    controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                    isValidName = true;
                }

                return isValidName;
            }
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Show")]
        public ActionResult Show(FormCollection collection)
        {
            DateTime date = new DateTime();
            if (!String.IsNullOrEmpty(collection["dateInput"]))
                date = Convert.ToDateTime(collection["dateInput"]);
            try
            {
                return RedirectToAction("Index", "WeekMenu", new { date });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //[MultipleButton(Name = "action", Argument = "Edit")]
        //public void Edit(FormCollection form)
        //{

        //    RedirectToAction("Index", new DateTime());
        //}



        // GET: WeekMenu
        public ActionResult Index(DateTime? date)
        {
            if (date == null)
            {
                date = DateTime.Today;
            }
            IEnumerable<DishModelShortInfo> result = _daySetService.GetWeekInfo((DateTime) date);
            foreach (var item in result.Where(item => string.IsNullOrEmpty(item.ImagePath)))
            {
                item.ImagePath = DefaultPathToImage;
            }
            
            ViewBag.DateWeek = date.Value.ToShortDateString();
            
            return View(result);
        }


        //// GET: WeekMenu/Details/5
        //public ActionResult Details(int id)
        //{


        //    return View();
        //}

        // GET: WeekMenu/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: WeekMenu/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: WeekMenu/Edit/5
        public ActionResult Edit(DateTime? date)
        {
            BigDishAndWeekDishSet toView = new BigDishAndWeekDishSet();

            var re = _dishService.GetAllDishes().ToList();
            //re.Where(r => string.IsNullOrEmpty(r.ImagePath)).ToList().ForEach(rr => rr.ImagePath = "../Dish/Common.gif");
            foreach (var item in re)
            {
                if (string.IsNullOrEmpty(item.ImagePath))
                {
                    item.ImagePath = DefaultPathToImage;
                }
            }


            var result = _daySetService.GetWeekInfo((DateTime)date);
            foreach (var item in result.Where(item => string.IsNullOrEmpty(item.ImagePath)))
            {
                item.ImagePath = DefaultPathToImage;
            }





            ViewBag.DateWeek = date.Value.ToShortDateString();
            toView.AllDishes = re;
            toView.CertainDishSets = result;
            return View(toView);
        }

        // POST: WeekMenu/Edit/5
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Edit")]
        public ActionResult Edit(FormCollection collection)
        {
            //var id = RouteData.Values["id"];
            var date = DateTime.Today;
            if (!String.IsNullOrEmpty(collection["dateInput"])) 
                date = Convert.ToDateTime(collection["dateInput"]);
            try
            {
                return RedirectToAction("Edit", "WeekMenu",new {date});
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //// GET: WeekMenu/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: WeekMenu/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public FileContentResult WeekSetToCsv(FormCollection collection)
        {
            string csv = String.Empty;
            foreach (var key in collection)
            {
                csv += key;
            }
            return File(new UTF8Encoding().GetBytes(csv), "text/csv", "Report123.csv");
       
        }

        public ActionResult ChangeWeek(FormCollection collection)
        {
            //WeekDishSetClassCall weekDish = new WeekDishSetClassCall();
            if (string.IsNullOrEmpty(collection["date"]))
            {
                return RedirectToAction("Index");
            }
            
            var dateTime = Convert.ToDateTime(collection["date"]);
            collection.Remove("date");

            string[] dishIds = new string[collection.Count];
            for (int i = 0; i < collection.Count; i++)
            {
                
                dishIds[i] = collection[i];
            }
            int[] ia = Array.ConvertAll(dishIds, int.Parse);
            //int[] ia = dishIds.Split(';').Select(n => Convert.ToInt32(n)).ToArray();

            _daySetService.DeleteAndEditWeekDishSet(dateTime,ia);
            
            return RedirectToAction("Index");
        }
    }
}
