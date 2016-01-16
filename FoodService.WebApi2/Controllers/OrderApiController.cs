using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.DAL.Entity;
using FoodService.WebApi2.Attribute;

namespace FoodService.WebApi2.Controllers
{
    [MyAuth]
    [RoutePrefix("api/order")]
    public class OrderApiController : ApiController
    {
        private readonly IOrderService _orderService;
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1);
        private static User user;
        int userId = 2;

        public OrderApiController(IOrderService service, IUserService userService)
        {
            _orderService = service;
            // here should get user entity from attr MyAuth
            user = userService.GetUser(userId);
        }

        //string email = "mike@gmail.com";
        

        [HttpGet]
        [Route("getuserset")]
        public HttpResponseMessage GetUserDishSetOnDay(long miliSecFrom1970)
        {

            //var userEmail = Thread.CurrentPrincipal.Identity.Name;
            var date = Jan1St1970.AddMilliseconds(miliSecFrom1970);
            var dayInfo = _orderService.GetPlatesByDate(date, user);
            return this.Request.CreateResponse(HttpStatusCode.OK, dayInfo);
        }

        [HttpPost]
        [Route("edituserset")]
        public HttpResponseMessage UpdateOrder(SetOnDay setOnDay)
        {
            _orderService.DeleteOldAndAddNewOrder(Jan1St1970.AddMilliseconds(setOnDay.Date), setOnDay.DishId, user);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
