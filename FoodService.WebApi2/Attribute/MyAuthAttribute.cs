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
        string role;

        public MyAuthAttribute(string role = "user")
        {
            this.role = role;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            bool auth = false;
            IEnumerable<string> headerValues;
            try
            {
                headerValues = actionContext.Request.Headers.GetValues("Token");
            }
            catch (Exception)
            {
                return false;
            }

            var _userService = ServiceLocator.Current.GetInstance<IUserService>();
            
            var requestToken = headerValues.FirstOrDefault();

            var secretKey = "G";
            try
            {

                //var jsonPayload = JsonWebToken.DecodeToObject(requestToken, secretKey) as IDictionary<string, object>;
                //string email = jsonPayload["email"].ToString();
                var email = requestToken;

                var user = _userService.GetUserInfo(email);

                if (user == null || (user.Role != role && user.Role != "admin")) return auth;

                GenericIdentity MyIdentity = new GenericIdentity(user.EmailAddress);

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