namespace EShopping.Controllers
{
    using EShoppingModel.Response;
    using EShoppingModel.Dto;
    using EShoppingRepository.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Cors;

    [Route("/bookstore/admin")]
    [ApiController]
    [EnableCors("CORS")]
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
                    var token = AdminService.GenerateJSONWebToken(adminData.id);
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Admin Found", adminData.fullName,token));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null,""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Not An Admin", null, ""));
        }

        [HttpPost, Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("book")]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto,[FromHeader]string token)
        {
            string adminData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                adminData = await Task.FromResult(AdminService.AddBook(bookDto));
                if (!adminData.Contains("Not") && adminData != null)
                {
                    if (!adminData.Contains("Invalid"))
                    {
                        adminData = "Book Added Successfully";
                    }
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, adminData, bookDto, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, adminData, bookDto, ""));
        }

        [HttpPost, Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("book/{bookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto bookDto,int bookId, [FromHeader]string token)
        {
            string adminData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                adminData = await Task.FromResult(AdminService.UpdateBook(bookDto));
                if (!adminData.Contains("Not") && adminData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, adminData, bookDto, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, adminData, bookDto, ""));
        }

        [HttpDelete, Authorize(AuthenticationSchemes =
            Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Route("delete/{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId,[FromHeader]string token)
        {
            string adminData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                adminData = await Task.FromResult(AdminService.DeleteBook(bookId));
                if (!adminData.Contains("Not") && adminData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, adminData, bookId, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, adminData, bookId, ""));
        }
    }
}
