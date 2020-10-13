namespace EShopping.Controllers
{
    using EShoppingModel.Response;
    using EShoppingModel.Dto;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Cors;
    using System.Net.Http;

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
                    var token = AdminService.GenerateJSONWebToken(adminData);
                    Response.Headers.Add("authorization",token);
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Admin Found", adminData.fullName));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Not An Admin", null));
        }

        [HttpPost]
        [Route("book")]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto,[FromHeader]string token)
        {
            try
            {
                var adminData = await Task.FromResult(AdminService.AddBook(bookDto));
                if (adminData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Book Added Successfully", adminData));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Book Not Added", null));
        }

        [HttpPut]
        [Route("book/{bookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto bookDto,int bookId, [FromHeader]string token)
        {
            var adminData = await Task.FromResult(AdminService.UpdateBook(bookDto));
            try
            {
                if (adminData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, adminData, bookDto));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, adminData, bookDto));
        }

        [HttpDelete]
        [Route("delete/{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId,[FromHeader]HttpRequestMessage httpRequestHeader)
        {
            var adminData = await Task.FromResult(AdminService.DeleteBook(bookId));
            try
            {
                if (adminData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, adminData, bookId));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, adminData, bookId));
        }
    }
}
