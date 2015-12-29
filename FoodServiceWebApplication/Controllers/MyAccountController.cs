using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;

namespace FoodServiceWebApplication.Controllers
{
    public class MyAccountController : Controller
    {

        private readonly IUserService _user;

        public MyAccountController(IUserService user)
        {
            _user = user;

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LogInUser model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Salt = model.Salt.GetHashCode().ToString();
            var boolLogin = _user.Login(model);


            


            if (boolLogin)
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToAction("Index", "Order");
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                model.Salt = model.Salt.GetHashCode().ToString();
                _user.CreateUser(model);
                LogInUser a = new LogInUser() {Email = model.EmailAddress, Salt = model.Salt};
                return Login(a, "");
            }

            return View(model);
        }










        private void AuthenticateThisRequest()
        {
            //NOTE:  if the user is already logged in (e.g. under a different user account)
            //       then this will NOT reset the identity information.  Be aware of this if
            //       you allow already-logged in users to "re-login" as different accounts 
            //       without first logging out.
            if (HttpContext.User.Identity.IsAuthenticated) return;

            var name = FormsAuthentication.FormsCookieName;
            var cookie = Response.Cookies[name];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !ticket.Expired)
                {
                    string[] roles = (ticket.UserData as string ?? "").Split(',');
                    HttpContext.User = new GenericPrincipal(new FormsIdentity(ticket), roles);
                }
            }
        }


    }









}