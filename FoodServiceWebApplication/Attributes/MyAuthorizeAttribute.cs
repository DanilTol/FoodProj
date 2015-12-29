using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using Microsoft.Practices.ServiceLocation;

namespace FoodServiceWebApplication.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        string name;
        
        public MyAuthorizeAttribute(string name = "user")
        {
            this.name = name;
        }





        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var _userService = ServiceLocator.Current.GetInstance<IUserService>();
            
            bool authorize = false;
            HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];//.ASPXAUTH
            if (authCookie == null) return authorize;
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var email = authTicket.Name;
            var user = _userService.GetUserInfo(email);
            if (user == null||(user.Role != name && user.Role != "admin")) return authorize;
            
            authorize = true;
            GenericIdentity MyIdentity = new GenericIdentity(user.EmailAddress);

            // Create generic principal.
            string[] MyStringArray = {user.Role};
            GenericPrincipal MyPrincipal =
                new GenericPrincipal(MyIdentity, MyStringArray);

            
            Thread.CurrentPrincipal = MyPrincipal;
            //IPrincipal s = MyPrincipal;
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult();
            filterContext.Result = new RedirectResult("~/MyAccount/Login");
        }


    }
}