namespace EShopping.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingModel.Response;
    using EShoppingRepository.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("/bookstore")]
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
        [Route("login")]
        [Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                string UserRole = this.GetUserRole();
                var UserData = await GetLogin(UserRole,loginDto);
                string message = UserData.userRole == 0 ? "Admin Found" : "User Found";
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

        [HttpPost]
        [Route("user/registration")]
        [Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
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
        private string GetUserRole()
        {
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            return roles.First();
        }
        private Task<User> GetLogin(string role,LoginDto loginDto)
        {
            if(role == "Admin")
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