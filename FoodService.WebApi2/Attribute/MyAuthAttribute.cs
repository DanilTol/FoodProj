using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using FoodService.Business.ServiceInterfaces;
using JWT;
using Microsoft.Practices.ServiceLocation;

namespace FoodService.WebApi2.Attribute
{
    public class MyAuthAttribute : AuthorizeAttribute
    {
        readonly string _role;

        public MyAuthAttribute(string role = "user")
        {
            this._role = role;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool auth = false;
            string requestToken;
            try
            {
                requestToken = actionContext.Request.Headers.GetValues("Token").FirstOrDefault();
            }
            catch (Exception)
            {
                return auth;
            }

            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            var secretKey = "G";
            try
            {

                var jsonPayload = JsonWebToken.DecodeToObject(requestToken, secretKey) as IDictionary<string, object>;
                var userId = Int32.Parse(jsonPayload["id"].ToString());

                var user = userService.GetUser(userId);

                if (user == null || (user.Role != _role && user.Role != "admin")) return auth;

                GenericIdentity MyIdentity = new GenericIdentity(user.Id.ToString());

                // Create generic principal.
                string[] MyStringArray = { user.Role };
                GenericPrincipal MyPrincipal =
                    new GenericPrincipal(MyIdentity, MyStringArray);
                
                Thread.CurrentPrincipal = MyPrincipal;

                auth = true;

            }
            catch (SignatureVerificationException)
            {
                auth = false;
            }

            return auth;
        }

        /// <summary>
        /// Processes requests that fail authorization.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}