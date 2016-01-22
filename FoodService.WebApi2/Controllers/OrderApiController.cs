using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            user = userService.GetUserEntity(userId);
        }

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
            _orderService.UpdateOrder(Jan1St1970.AddMilliseconds(setOnDay.Date), setOnDay.DishId, setOnDay.DishNum, user);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("orderlist")]
        public HttpResponseMessage GetListOfOrders(long miliSecFrom1970)
        {
            var ordersList = _orderService.GetOrderListOnWeek(Jan1St1970.AddMilliseconds(miliSecFrom1970).Date);
            return Request.CreateResponse(HttpStatusCode.OK, ordersList);
        }

        [HttpPost]
        [Route("ordersdelete")]
        public HttpResponseMessage DeleteRangeOrders(int[] orderInfos)
        {
            _orderService.DeleteRangeOrders(orderInfos);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("sentmail")]
        public HttpResponseMessage SentOrderToChef(long miliSecFrom1970, string chefMail)
        {
            _orderService.SentMailToChef(Jan1St1970.AddMilliseconds(miliSecFrom1970).Date,chefMail);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("notificationcheckorders")]
        public HttpResponseMessage NotificationCheckedOrders()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _orderService.NumberOfUnchecked());
        }

        [HttpGet]
        [Route("uncheckorders")]
        public HttpResponseMessage NotCheckedOrders()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _orderService.NotCheckedOrders());
        }

    }
}
