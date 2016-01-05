using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Infrastructure.Core;

namespace FoodService.WebApi2.Controllers
{
    [RoutePrefix("api/dishset")]
    public class DishSetApiController : ApiControllerBase
    {
        private readonly IDaySetService _daySetService;

       public DishSetApiController(IDaySetService service)
       {
           _daySetService = service;
       }

            [HttpGet]
        [Route("getdishmenu/{date:DateTime}")]
        public HttpResponseMessage GetDishMenuOnDay(DateTime date)
        {
            return CreateHttpResponse(this.Request, () =>
            {
                var detailsInfo = _daySetService.GetDayInfo(date);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            });
        }

        // GET: api/DishMenuApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DishMenuApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DishMenuApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DishMenuApi/5
        public void Delete(int id)
        {
        }
    }
}
