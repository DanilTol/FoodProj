using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Attribute;

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

        [MyAuth]
        [HttpGet]
        [Route("filterdishmenu")]
        public HttpResponseMessage GetFilterDishMenuOnDay(long miliSecFrom1970, string filter = "")
        {
            var dayInfo = _daySetService.Filter(Jan1St1970.AddMilliseconds(miliSecFrom1970), filter);
            return this.Request.CreateResponse(HttpStatusCode.OK, dayInfo);
        }

        [MyAuth("admin")]
        [HttpPost]
        [Route("editdishmenu")]
        public HttpResponseMessage UpdatingMenuOnDay(SetOnDay setOnDay)
        {
            _daySetService.UpdateDayDishSet(Jan1St1970.AddMilliseconds(setOnDay.Date),setOnDay.DishId);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
