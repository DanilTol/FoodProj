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
        private const string secretKey = "G";

        public AccountController(IUserService user)
        {
            _userService = user;
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(HttpRequestMessage request, LogInUser user)
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
                string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
                token = user.Email;
                response = request.CreateResponse<string>(HttpStatusCode.OK, token);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return response;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public HttpResponseMessage Register(HttpRequestMessage request, UserDTO newUser)
        {
            if (ModelState.IsValid)
            {
                newUser.Salt = newUser.Salt.GetHashCode().ToString();
                _userService.CreateUser(newUser);
                LogInUser a = new LogInUser() { Email = newUser.EmailAddress, Salt = newUser.Salt };
                return Login(request, a);
            }
            return request.CreateResponse(HttpStatusCode.BadRequest);
        }


        [HttpGet]
        [MyAuth]
        [Route("profileInfo")]
        public HttpResponseMessage LoginUserName(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues = request.Headers.GetValues("Token");
            var requestToken = headerValues.FirstOrDefault();

            //TODO:decode token

            var userName = _userService.GetUserInfo(requestToken);

            return request.CreateResponse<UserDTO>(HttpStatusCode.OK, userName);
        }
    }
}
