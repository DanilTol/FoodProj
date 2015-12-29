using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Attribute;
using FoodService.WebApi2.Infrastructure.Core;
using FoodService.WebApi2.Models;

namespace FoodService.WebApi2.Controllers
{
    [RoutePrefix("api/dishes")]
    public class DishApiController : ApiControllerBase
    {
        private readonly IDishService _dishService;

        public DishApiController(IDishService service)
        {
            _dishService = service;
        }


        ////GET: api/DishApi
        //[MyAuth]
        //public HttpResponseMessage Get(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var re = _dishService.GetAllDishes().ToList();
        //        return request.CreateResponse<IEnumerable<DishModelShortInfo>>(HttpStatusCode.OK, re);
        //    });
        //}


        // GET: api/DishApi/5
        [MyAuth]
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var detailsInfo = _dishService.GetDishById(id);
                return request.CreateResponse<DishModelDetailsInfo>(HttpStatusCode.OK, detailsInfo);
            });
        }




        //[AllowAnonymous]
        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search(int page = 1, int pageSize = 2, string filter = null)
        {
            
            int currentPage = page;
            int currentPageSize = pageSize;

            return CreateHttpResponse(this.Request, () =>
            {
                HttpResponseMessage response = null;

                var shortDish = _dishService.FilterDishes(currentPage, currentPageSize, filter);
                var totalDishes = _dishService.TotalFilteredDish(filter);
                
                PaginationSet<DishModelShortInfo> pagedSet = new PaginationSet<DishModelShortInfo>
                {
                    Page = currentPage,
                    TotalCount = totalDishes,
                    TotalPages = (int)Math.Ceiling((decimal)totalDishes / currentPageSize),
                    Items = shortDish
                };

                response = this.Request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }









        // POST: api/DishApi
        [MyAuth("admin")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddDish(HttpRequestMessage request, DishModelDetailsInfo detailsDish)
        {
            return CreateHttpResponse(request, () =>
           {
               HttpResponseMessage response;

               if (!ModelState.IsValid)
               {
                   response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
               }
               else
               {
                   _dishService.CreateDish(detailsDish);
                   response = request.CreateResponse<DishModelDetailsInfo>(HttpStatusCode.Created, detailsDish);
               }

               return response;
           });
        }





        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, DishModelDetailsInfo dish)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dishDb = _dishService.GetDishById(dish.ID);
                    if (dishDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid movie.");
                    else
                    {
                        _dishService.EditDish(dish);
                        response = request.CreateResponse<DishModelDetailsInfo>(HttpStatusCode.OK, dish);
                    }
                }

                return response;
            });
        }
    }
}
