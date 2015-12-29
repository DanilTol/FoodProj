using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using FoodService.Business.Services;
using FoodService.DAL;
using FoodService.WebApi2.Controllers;

namespace FoodService.WebApi2.Dependency
{
    public class PoorMansCompositionRoot : IHttpControllerActivator
    {
        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            if (controllerType == typeof(DishApiController))
                return new DishApiController(
                    new DishService(new UnityOfWork()));
            if (controllerType == typeof(AccountController))
            {
                return new AccountController(
                    new UserService(new UnityOfWork()));
            }

            return null;
        }
    }
}