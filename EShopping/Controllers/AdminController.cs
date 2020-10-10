namespace EShopping.Controllers
{
    using EShoppingModel.Response;
    using EShoppingRepository.Dto;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    [Route("/bookstore/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public AdminController(IAdminService service)
        {
            this.AdminService = service;
        }
        public IAdminService AdminService { get; set; }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDto loginDto)
        {
            try
            {
                var adminData = await Task.FromResult(AdminService.AdminLogin(loginDto));
                if (adminData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Admin Found", adminData));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Not An Admin", null));
        }
    }
}
