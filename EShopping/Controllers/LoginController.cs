namespace EShopping.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using EShoppingModel.Dto;
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
                var UserData = await Task.FromResult(AdminService.AdminLogin(loginDto));
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
    }
}
