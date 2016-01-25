using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.WebApi2.Attribute;
using JWT;

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
        public HttpResponseMessage Login( LogInUser user)
        {
            HttpResponseMessage response;
            var existUser = _userService.Login(new LogInUser()
            { Email = user.Email, Salt = user.Salt.GetHashCode().ToString() });
            if (existUser != null)
            {
                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var exp = Math.Round((DateTime.UtcNow.AddDays(1) - unixEpoch).TotalSeconds);

                var payload = new Dictionary<string, object>()
                {
                    {"id", existUser.id},
                    {"pass", existUser.Salt },
                    {"exp", exp}
                };
                string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
                response = this.Request.CreateResponse<string>(HttpStatusCode.OK, token);
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return response;
        }


        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(UserDTO newUser)
        {
            if (!ModelState.IsValid) return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            newUser.Salt = newUser.Salt.GetHashCode().ToString();
            if (!_userService.CreateUser(newUser))
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            LogInUser a = new LogInUser() { Email = newUser.EmailAddress, Salt = newUser.Salt };
            return Login(a);
        }

        [MyAuth("admin")]
        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAllUsers( )
        {
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetAll(Int32.Parse(Thread.CurrentPrincipal.Identity.Name)));
        }


        [HttpGet]
        [Route("profileInfo")]
        public HttpResponseMessage LoginUserName()
        {
            try
            {
                var requestToken = this.Request.Headers.GetValues("Token").FirstOrDefault();

                var jsonPayload = JsonWebToken.DecodeToObject(requestToken, secretKey) as IDictionary<string, object>;
                var userId = Int32.Parse(jsonPayload["id"].ToString());

                var userInfo = _userService.GetUser(userId);

                return this.Request.CreateResponse(HttpStatusCode.OK, userInfo);
            }
            catch (Exception)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
        }

        [HttpPost]
        [Route("edit")]
        public HttpResponseMessage EditProfile(UserEdit newProfileInfo)
        {
            bool edited = _userService.EditUser(newProfileInfo);
            return Request.CreateResponse(edited ? HttpStatusCode.Accepted : HttpStatusCode.Forbidden);
        }

        [HttpPost]
        [Route("editasadmin")]
        public HttpResponseMessage EditAsAdmin(UserEdit newProfileInfo)
        {
            _userService.EditAsAdmin(newProfileInfo);
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [HttpDelete]
        [Route("deleteUser")]
        public HttpResponseMessage DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }


    }
}
