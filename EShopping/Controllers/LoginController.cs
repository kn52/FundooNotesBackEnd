namespace EShopping.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingModel.Response;
    using EShoppingRepository.Infc;
    using Microsoft.AspNetCore.Mvc;

    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController(IAdminService adminService,IUserService userService)
        {
            this.AdminService = adminService;
            this.UserService = userService;
        }
        public IAdminService AdminService { get; set; }
        public IUserService UserService { get; set; }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto,[FromQuery] string userRole = "User")
        {
            try
            {
                var UserData = await GetLogin(userRole.Trim().ToLower(),loginDto);
                string message = UserData != null && UserData.userRole == 0 ? "Admin Found" : "User Found";
                if (UserData != null)
                {
                    var token = this.GenerateToken(UserData);
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, message, UserData.fullName, token));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "User Not Found", null, ""));
        }
        private Task<User> GetLogin(string userRole,LoginDto loginDto)
        {
            if(userRole == "admin")
            {
                return Task.FromResult(AdminService.AdminLogin(loginDto));
            }
            return Task.FromResult(UserService.UserLogin(loginDto));
        }
        private string GenerateToken(User user)
        {
            if (user.userRole == 0)
            {
                return AdminService.GenerateJSONWebToken(user.id, "Admin");
            }
            return UserService.GenerateJSONWebToken(user.id, "User");
        }
    }
}