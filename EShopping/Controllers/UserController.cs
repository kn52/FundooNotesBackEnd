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
    using System.Net.Http;

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
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Headers.Add("Autherization", "Hello");
                    return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, userRegistrationDto));
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
        [Route("verify/email/{token}")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var UserData = await Task.FromResult(UserService.VerifyUserEmail(token));
            try
            {
                if (UserData.Contains("Verified") && UserData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.Found, UserData, UserData));
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
                    return this.Ok(new ResponseEntity(HttpStatusCode.Found, "User Found", UserData));
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Not Found ", null));
        }
    }
}
