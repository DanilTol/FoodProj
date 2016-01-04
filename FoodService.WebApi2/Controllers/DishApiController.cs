using System;
using System.Net;
using System.Net.Http;
using System.Web;
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

        // GET: api/DishApi/5
        //[MyAuth]
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            return CreateHttpResponse(this.Request, () =>
            {
                var detailsInfo = _dishService.GetDishById(id);
                return this.Request.CreateResponse<DishModelDetailsInfo>(HttpStatusCode.OK, detailsInfo);
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
                var shortDish = _dishService.FilterDishes(currentPage, currentPageSize, filter);
                var totalDishes = _dishService.TotalFilteredDish(filter);
                
                PaginationSet<DishModelShortInfo> pagedSet = new PaginationSet<DishModelShortInfo>
                {
                    Page = currentPage,
                    TotalCount = totalDishes,
                    TotalPages = (int)Math.Ceiling((decimal)totalDishes / currentPageSize),
                    Items = shortDish
                };

                return this.Request.CreateResponse(HttpStatusCode.OK, pagedSet);
            });
        }


        // POST: api/DishApi
        //[MyAuth("admin")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddDish(DishModelDetailsInfo detailsDish, HttpPostedFileBase dishImg)
        {
            return CreateHttpResponse(this.Request, () =>
           {
               HttpResponseMessage response;

               if (!ModelState.IsValid)
               {
                   response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
               }
               else
               {
                   _dishService.CreateDish(detailsDish);
                   response = this.Request.CreateResponse<DishModelDetailsInfo>(HttpStatusCode.Created, detailsDish);
               }

               return response;
           });
        }


        [HttpPost]
        [Route("imageup")]
        public HttpResponseMessage UploadImage(HttpPostedFileBase dishImg)
        {
            return CreateHttpResponse(this.Request, () =>
            {
                HttpResponseMessage response;

                if (!ModelState.IsValid)
                {
                    response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }


        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(DishModelDetailsInfo dish)
        {
            return CreateHttpResponse(this.Request, () =>
            {
                HttpResponseMessage response;

                if (!ModelState.IsValid)
                {
                    response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dishDb = _dishService.GetDishById(dish.ID);
                    if (dishDb == null)
                        response = this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid movie.");
                    else
                    {
                        _dishService.EditDish(dish);
                        response = this.Request.CreateResponse<DishModelDetailsInfo>(HttpStatusCode.OK, dish);
                    }
                }

                return response;
            });
        }
    }
}
