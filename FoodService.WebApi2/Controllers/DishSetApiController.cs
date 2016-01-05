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

        public DishSetApiController(IDaySetService service)
        {
            _daySetService = service;
        }

        [HttpGet]
        [Route("getdishmenu/{date:int}")]
        public HttpResponseMessage GetDishMenuOnDay(int date)
        {
            var dayInfo = _daySetService.GetDayInfo(new DateTime(date));
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
