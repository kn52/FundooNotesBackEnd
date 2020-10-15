namespace EShopping.Controllers
{
    using System;
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

        // POST: api/User
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var UserData = await Task.FromResult(UserService.UserRegistration(userRegistrationDto));
            try
            {
                if (UserData.Contains("Success") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Registered Successfully.Email Verification " +
                        "Link Is Sent To Your Registered Email Id", UserData));
                }
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Email Already Exist", UserData));
        }

        [HttpPost]
        [Route("verify/email/")]
        public async Task<IActionResult> VerifyEmail([FromQuery]string token)
        {
            var UserData = await Task.FromResult(UserService.VerifyUserEmail(token));
            try
            {
                if (UserData.Contains("Verified") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, UserData));
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
        {
            var UserData = await Task.FromResult(UserService.UserLogin(loginDto));
            try
            {
                if (UserData != null)
                {
                    var token = UserService.GenerateJSONWebToken(UserData.id);
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Login Successfully", UserData));
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Not Found ", null));
        }

        [HttpPost]
        [Route("reset/password/")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto,[FromQuery]string token)
        {
            var UserData = await Task.FromResult(UserService.ResetPassword(resetPasswordDto));
            try
            {
                if (UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, UserData, resetPasswordDto));
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, null));
        }
    }
}
