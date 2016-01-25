using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Attribute;

namespace FoodService.WebApi2.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderApiController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1);

        public OrderApiController(IOrderService service, IUserService userService)
        {
            _orderService = service;
            _userService = userService;
        }

        [MyAuth]
        [HttpGet]
        [Route("getuserset")]
        public HttpResponseMessage GetUserDishSetOnDay(long miliSecFrom1970)
        {
            var user = _userService.GetUserEntity(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            //var userEmail = Thread.CurrentPrincipal.Identity.Name;
            var date = Jan1St1970.AddMilliseconds(miliSecFrom1970);
            var dayInfo = _orderService.GetPlatesByDate(date, user);
            return this.Request.CreateResponse(HttpStatusCode.OK, dayInfo);
        }

        [MyAuth]
        [HttpPost]
        [Route("edituserset")]
        public HttpResponseMessage UpdateOrder(SetOnDay setOnDay)
        {
            var user = _userService.GetUserEntity(Int32.Parse(Thread.CurrentPrincipal.Identity.Name));
            _orderService.UpdateOrder(Jan1St1970.AddMilliseconds(setOnDay.Date), setOnDay.DishId, setOnDay.DishNum, user);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        [MyAuth("admin")]
        [HttpGet]
        [Route("orderlist")]
        public HttpResponseMessage GetListOfOrders(long miliSecFrom1970)
        {
            var ordersList = _orderService.GetOrderListOnWeek(Jan1St1970.AddMilliseconds(miliSecFrom1970).Date);
            return Request.CreateResponse(HttpStatusCode.OK, ordersList);
        }

        [MyAuth("admin")]
        [HttpPost]
        [Route("ordersdelete")]
        public HttpResponseMessage DeleteRangeOrders(int[] orderInfos)
        {
            _orderService.DeleteRangeOrders(orderInfos);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MyAuth("admin")]
        [HttpGet]
        [Route("notificationcheckorders")]
        public HttpResponseMessage NotificationCheckedOrders()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _orderService.NumberOfUnchecked());
        }

        [MyAuth("admin")]
        [HttpGet]
        [Route("uncheckorders")]
        public HttpResponseMessage NotCheckedOrders()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _orderService.NotCheckedOrders());
        }

    }
}
