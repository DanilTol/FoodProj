using System;
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
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1);


        public DishSetApiController(IDaySetService service)
        {
            _daySetService = service;
        }

        [HttpGet]
        [Route("getdishmenu")]
        public HttpResponseMessage GetDishMenuOnDay(long miliSecFrom1970)
        {
            var date = Jan1St1970.AddMilliseconds(miliSecFrom1970);
            var dayInfo = _daySetService.GetDayInfo(date);
            return this.Request.CreateResponse(HttpStatusCode.OK, dayInfo);
        }

        [HttpPost]
        [Route("editdishmenu")]
        //[Route("editdishmenu/{date:DateTime}")]
        public HttpResponseMessage UpdatingMenuOnDay(SetOnDay setOnDay)
        {
            _daySetService.DeleteAndEditDayDishSet(Jan1St1970.AddMilliseconds(setOnDay.Date),setOnDay.DishId);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
