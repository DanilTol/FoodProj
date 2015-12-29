using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FoodService.Business.ServiceInterfaces;
using FoodServiceWebApplication.Attributes;

namespace FoodServiceWebApplication.Controllers
{
    //[Authorize]
    public class MyManageController : Controller
    {
        private readonly IUserService _userService;

        public MyManageController(IUserService service)
        {
            _userService = service;
        }

        [MyAuthorize()]
        public ActionResult Index()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];//.ASPXAUTH
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var a = Thread.CurrentPrincipal;

            var s = this.ControllerContext.HttpContext.User.Identity.Name;

            var userdata = _userService.GetUserInfo(a.Identity.Name);
            return View(userdata);
        }

        [MyAuthorize("admin")]
        public ActionResult ChangePass()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(FormCollection collection)
        {
            var pass = collection["oldPass"];
            var newpass = collection["newPass"];
            var name = "";
            var userdata = _userService.GetUserInfo(name);
            var oldPass = pass.GetHashCode().ToString();
            if (userdata.Salt == oldPass)
            {
                userdata.Salt = newpass.GetHashCode().ToString();
                _userService.EditUser(userdata);
                return RedirectToAction("Index");
            }
            return RedirectToAction("ChangePass");
        }


    }
}