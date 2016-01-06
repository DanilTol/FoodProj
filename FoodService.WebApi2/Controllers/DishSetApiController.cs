using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;

namespace FoodService.WebApi2.Controllers
{
    [RoutePrefix("api/dishset")]
    public class DishSetApiController : ApiController
    {
        private readonly IDaySetService _daySetService;
        private readonly IDishService _dishService;

        public DishSetApiController(IDaySetService service, IDishService serv)
        {
            _daySetService = service;
            _dishService = serv;
        }

        [HttpGet]
        [Route("getdishmenu")]
        public HttpResponseMessage GetDishMenuOnDay(string datestr)
        {
            //var date =Convert.ToDateTime(datestr);
            //var datestr = DateTime.Now.ToString();

            var date = DateTime.ParseExact("Wed Dec 16 00:00:00 UTC-0400 2009",
                                  "ddd MMM d HH:mm:ss UTCzzzzz yyyy",
                                  CultureInfo.InvariantCulture);


            var dayInfo1 = _daySetService.GetDayInfo(date);

            var dayInfo = _dishService.GetAllDishes();
            return this.Request.CreateResponse(HttpStatusCode.OK, dayInfo);
        }

        [HttpPost]
        [Route("editdishmenu")]
        //[Route("editdishmenu/{date:DateTime}")]
        public HttpResponseMessage UpdatingMenuOnDay(SetOnDay setOnDay)
        {
            _daySetService.DeleteAndEditDayDishSet(setOnDay.Date,setOnDay.DishId);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
