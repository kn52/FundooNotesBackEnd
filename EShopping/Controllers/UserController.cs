namespace EShopping.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using EShoppingModel.Response;
    using EShoppingModel.Dto;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Cors;
    
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
                        "Link Is Sent To Your Registered Email Id", UserData));
                }
                
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, ""));
        }

        [HttpPost]
        [Route("verify/email/")]
        public async Task<IActionResult> VerifyEmail([FromQuery]string token)
        {
            string UserData;
            try
            {
                UserData = await Task.FromResult(UserService.VerifyUserEmail(token));
                if (!UserData.Contains("Not") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, ""));
                }

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null));
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
                    Response.Headers.Add("authorization", token);
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Login Successfully", UserData));
                }

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Not Found ", null));
        }

        [HttpPost]
        [Route("forget/password/")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            string UserData;
            try
            {
                UserData = await Task.FromResult(UserService.ForgetPassword(email));
                if (!UserData.Contains("Not") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, email));
                }

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null));
        }

        [HttpPost]
        [Route("reset/password/")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto,[FromQuery]string token)
        {
            string UserData;
            try
            {
                UserData = await Task.FromResult(UserService.ResetPassword(resetPasswordDto, token));
                if (UserData.Contains("Success") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, resetPasswordDto));
                } 

            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null));
        }
    }
}
