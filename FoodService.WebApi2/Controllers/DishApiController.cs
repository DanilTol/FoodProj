using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Attribute;
using FoodService.WebApi2.Infrastructure.Core;
using FoodService.WebApi2.Models;
using FoodService.WebApi2.Infrastructure;

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
        [HttpGet]
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            return CreateHttpResponse(this.Request, () =>
            {
                var detailsInfo = _dishService.GetDishById(id);
                return this.Request.CreateResponse(HttpStatusCode.OK, detailsInfo);
            });
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search(int page = 1, int pageSize = 2, string filter = null)
        {
            var k = HttpContext.Current.User;
            var z = Thread.CurrentPrincipal;
            var c = Membership.GetAllUsers();
            var shortDish = _dishService.FilterDishes(page, pageSize, filter);
            var totalDishes = _dishService.TotalFilteredDish(filter);

            PaginationSet<DishModelShortInfo> pagedSet = new PaginationSet<DishModelShortInfo>
            {
                Page = page,
                TotalCount = totalDishes,
                TotalPages = (int) Math.Ceiling((decimal) totalDishes/pageSize),
                Items = shortDish
            };

            return this.Request.CreateResponse(HttpStatusCode.OK, pagedSet);
        }

        [MyAuth("admin")]
        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Images/Dish");
            var provider = new CustomMultipartFormDataStreamProvider(root); //new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                var pathArray =
                    provider.FileData.Select(file => file.LocalFileName.Substring(root.Length + 1)).ToArray();

                var detailsDish = new DishModelDetailsInfo
                {
                    Description = provider.FormData.Get("Description"),
                    Energy = Convert.ToInt32(provider.FormData.Get("Energy")),
                    ImagePath = pathArray,
                    Ingridients = provider.FormData.Get("Ingridients"),
                    Name = provider.FormData.Get("Name"),
                    Price = Convert.ToInt32(provider.FormData.Get("Price")),
                    Weight = Convert.ToInt32(provider.FormData.Get("Weight"))
                };

                _dishService.CreateDish(detailsDish);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        
        [MyAuth("admin")]
        [HttpPost]
        [Route("update")]
        public async Task<HttpResponseMessage> Update()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Images/Dish");
            var provider = new CustomMultipartFormDataStreamProvider(root); //new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                var pathArray =
                    provider.FileData.Select(file => file.LocalFileName.Substring(root.Length + 1)).ToArray();

                var detailsDish = new DishModelDetailsInfo
                {
                    ID = Convert.ToInt32(provider.FormData.Get("id")),
                    Description = provider.FormData.Get("Description"),
                    Energy = Convert.ToInt32(provider.FormData.Get("Energy")),
                    ImagePath = pathArray,
                    Ingridients = provider.FormData.Get("Ingridients"),
                    Name = provider.FormData.Get("Name"),
                    Price = Convert.ToInt32(provider.FormData.Get("Price")),
                    Weight = Convert.ToInt32(provider.FormData.Get("Weight"))
                };

                //if (provider.FormFile)
                //{
                //    detailsDish.ImagePath = provider.FormData.Get("ImagePath");
                //}


                _dishService.EditDish(detailsDish);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }



            //return CreateHttpResponse(this.Request, () =>
            //{
            //    HttpResponseMessage response;

            //    if (!ModelState.IsValid)
            //    {
            //        response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //    }
            //    else
            //    {
            //        var dishDb = _dishService.GetDishById(dish.ID);
            //        if (dishDb == null)
            //            response = this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid dish.");
            //        else
            //        {
            //            _dishService.EditDish(dish);
            //            response = this.Request.CreateResponse<DishModelDetailsInfo>(HttpStatusCode.OK, dish);
            //        }
            //    }

            //    return response;
            //});
        }

        [MyAuth("admin")]
        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage Delete(int dishId = 2)
        {
            _dishService.DeleteDish(dishId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
