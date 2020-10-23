namespace EShopping.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using EShoppingModel.Response;
    using EShoppingModel.Dto;
    using EShoppingRepository.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Cors;
    using System.ComponentModel;

    [Route("/bookstore/user")]
    [ApiController]
    [EnableCors("CORS")]
    public class UserController : ControllerBase
    {
        public UserController(IUserService service)
        {
            this.UserService = service;
        }
        IUserService UserService { get; set; }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            string UserData; 
            try
            {
                UserData = await Task.FromResult(UserService.UserRegistration(userRegistrationDto));
                if (UserData != "" && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Registered Successfully.Email Verification " +
                        "Link Is Sent To Your Registered Email Id", UserData, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, "", ""));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("verify/email/")]
        public async Task<IActionResult> VerifyEmail()
        {
            string UserData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                UserData = await Task.FromResult(UserService.VerifyUserEmail(userId));
                if (!UserData.Contains("Not") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, "", ""));
                }

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null, ""));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
        {
            try
            {
                var UserData = await Task.FromResult(UserService.UserLogin(loginDto));
                if (UserData != null)
                {
                    var token = UserService.GenerateJSONWebToken(UserData.id);
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Login Successfully", UserData, token));
                }

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Not Found ", null, ""));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("forget/password/")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            string UserData;
            try
            {
                UserData = await Task.FromResult(UserService.ForgetPassword(email));
                if (!UserData.Contains("Not") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, email, ""));
                }

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null, ""));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("reset/password/")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            string UserData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                UserData = await Task.FromResult(UserService.ResetPassword(resetPasswordDto, userId));
                if (UserData.Contains("Success") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, resetPasswordDto, ""));
                } 

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null, ""));
        }

        [HttpGet]
        [Route("detail")]
        public async Task<IActionResult> FetchUserDetail()
        {
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                var UserData = await Task.FromResult(UserService.FetchUserDetail(userId));
                if (UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "User Found", UserData, ""));
                }

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, "User Not Found ", null, ""));
        }
    }
}
