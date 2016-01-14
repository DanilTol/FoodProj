using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodServiceWebApplication.Attributes;

namespace FoodServiceWebApplication.Controllers
{
    [MyAuthorize]
    public class OrderController : Controller
    {
        private const string DefaultPathToImage = "../../Dish/Common.gif";


        private readonly IOrderService _orderService;
        private readonly IDaySetService _daySetService;

        public OrderController(IOrderService order, IDaySetService day)
        {
            _orderService = order;
            _daySetService = day;
        }


        // GET: Order
        public ActionResult Index(FormCollection collection)
        {
            IList<OrderAndAvaliableModel> realBig = new List<OrderAndAvaliableModel>();
            //OrderCall orderCall =new OrderCall();
            //DishService dishService = new DishService();
            //WeekDishSetClassCall dishSetClassCall = new WeekDishSetClassCall();
            var day = DateTime.Today;

            int delta = DayOfWeek.Monday - day.DayOfWeek;
            day = day.AddDays(delta);
            if (collection["date"] != null)
            {
                delta = DayOfWeek.Monday - DateTime.Parse(collection["date"]).DayOfWeek;
                if (delta==1)
                {
                    delta = -6;
                }
                day = DateTime.Parse(collection["date"]).AddDays(delta);
            }
    
            for (int i = 0; i < 5; i++)
            {
                IEnumerable<DishModelShortInfo> orderPlateses = _orderService.GetPlatesByDate(day.AddDays(i), Thread.CurrentPrincipal.Identity.Name);
                foreach (var item in orderPlateses)
                {
                    if (string.IsNullOrEmpty(item.ImagePath))
                    {
                        item.ImagePath = DefaultPathToImage;
                    }
                }
                IEnumerable<DishModelShortInfo> shroDishModelShortInfos = _daySetService.Filter(day.AddDays(i),"");
                foreach (var item in shroDishModelShortInfos)
                {
                    if (string.IsNullOrEmpty(item.ImagePath))
                    {
                        item.ImagePath = DefaultPathToImage;
                    }
                }
                realBig.Add(new OrderAndAvaliableModel
                {
                    OrderPlateses = orderPlateses,
                    SetOnDays = shroDishModelShortInfos
                });
            }

            ViewBag.RequestDate = day;
            return View(realBig);
        }
        
        public ActionResult Edit(FormCollection collection)
        {
            //OrderCall arCall = new OrderCall();
            var array = new List<string>
            {
                !String.IsNullOrEmpty(collection["mondaySet"]) ? collection["mondaySet"] : "0",
                !String.IsNullOrEmpty(collection["tuesdaySet"]) ? collection["tuesdaySet"] : "0",
                !String.IsNullOrEmpty(collection["wednesdaySet"]) ? collection["wednesdaySet"] : "0",
                !String.IsNullOrEmpty(collection["thursdaySet"]) ? collection["thursdaySet"] : "0",
                !String.IsNullOrEmpty(collection["fridaySet"]) ? collection["fridaySet"] : "0"
            };
            
            _orderService.EditOrder(Convert.ToDateTime(collection["dateOrder"]),array, Thread.CurrentPrincipal.Identity.Name);

            return RedirectToAction("Index");
        }
    }
}
