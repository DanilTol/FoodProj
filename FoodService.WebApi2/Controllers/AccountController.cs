using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Attribute;

namespace FoodService.WebApi2.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;
        private const string SecretKey = "G";

        public AccountController(IUserService user)
        {
            _userService = user;
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login( LogInUser user)
        {
            HttpResponseMessage response;
            var boolLogin = _userService.Login(new LogInUser()
            { Email = user.Email, Salt = user.Salt.GetHashCode().ToString() });
            if (boolLogin)
            {

                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var now = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);

                var payload = new Dictionary<string, object>()
                {
                    {"email", user.Email},
                    {"exp", now}
                };
                string token = JWT.JsonWebToken.Encode(payload, SecretKey, JWT.JwtHashAlgorithm.HS256);
                token = user.Email;
                response = this.Request.CreateResponse<string>(HttpStatusCode.OK, token);
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return response;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public HttpResponseMessage Register(UserDTO newUser)
        {
            if (ModelState.IsValid)
            {
                newUser.Salt = newUser.Salt.GetHashCode().ToString();
                _userService.CreateUser(newUser);
                LogInUser a = new LogInUser() { Email = newUser.EmailAddress, Salt = newUser.Salt };
                return Login(a);
            }
            return this.Request.CreateResponse(HttpStatusCode.BadRequest);
        }


        [HttpGet]
        [MyAuth]
        [Route("profileInfo")]
        public HttpResponseMessage LoginUserName()
        {
            IEnumerable<string> headerValues = this.Request.Headers.GetValues("Token");
            var requestToken = headerValues.FirstOrDefault();

            //TODO:decode token

            var userName = _userService.GetUserInfo(requestToken);

            return this.Request.CreateResponse(HttpStatusCode.OK, userName);
        }
    }
}
