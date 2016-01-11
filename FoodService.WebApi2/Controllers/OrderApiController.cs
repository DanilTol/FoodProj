using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;

namespace FoodService.WebApi2.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderApiController : ApiController
    {
        private readonly IOrderService _orderService;
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1);


        public OrderApiController(IOrderService service)
        {
            _orderService = service;
        }

        string email = "mike@gmail.com";

        [HttpGet]
        [Route("getuserset")]
        public HttpResponseMessage GetUserDishSetOnDay(long miliSecFrom1970)
        {
            var date = Jan1St1970.AddMilliseconds(miliSecFrom1970);
            var dayInfo = _orderService.GetPlatesByDate(date, email);
            return this.Request.CreateResponse(HttpStatusCode.OK, dayInfo);
        }

        [HttpPost]
        [Route("edituserset")]
        public HttpResponseMessage UpdateOrder(SetOnDay setOnDay)
        {
            _orderService.EditOrder(Jan1St1970.AddMilliseconds(setOnDay.Date), setOnDay.DishId, email);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
