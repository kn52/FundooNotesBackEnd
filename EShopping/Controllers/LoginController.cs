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
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var UserData = await GetLogin(this.GetUserRole(),loginDto);
                string message = UserData.userRole == 0 ? "Admin Found" : "User Found";
                if (UserData != null)
                {
                    var token = AdminService.GenerateJSONWebToken(UserData.id);
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, message, UserData.fullName, token));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "User Not Found", null, ""));
        }

        private Task<User> GetLogin(string role,LoginDto loginDto)
        {
            if(role == "Admin")
            {
                return Task.FromResult(AdminService.AdminLogin(loginDto));
            }
            return Task.FromResult(UserService.UserLogin(loginDto));
        }

        private string GetUserRole()
        {
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            return roles.First();
        }
    }
}
