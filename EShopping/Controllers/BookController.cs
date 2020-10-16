namespace EShopping.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using EShoppingModel.Response;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("/bookstore")]
    [ApiController]
    [EnableCors("CORS")]
    public class BookController : ControllerBase
    {
        public BookController(IBookService service)
        {
            this.BookService = service;
        }
        public IBookService BookService { get; set; }

        [HttpGet]
        [Route("books")]
        public async Task<IActionResult> GetBook()
        {
            try
            {
                var BookData = await Task.FromResult(BookService.GetBooks());
                if (BookData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Books Found", BookData));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "No Book Found",null));
        }

        [HttpGet]
        [Route("image/{filename}")]
        public async Task<String> UploadImage([FromForm]IFormFile file)
        {
            var name = await Task.FromResult(file.FileName);
            return name;
        }
    }
}
