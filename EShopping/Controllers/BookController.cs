namespace EShopping.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using EShoppingModel.Response;
    using EShoppingRepository.Infc;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [Route("books")]
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
        public async Task<IActionResult> GetBook(string searchBy = "",string filterBy = "name", string orderBy = "asc")
        {
            try
            {
                var BookData = await Task.FromResult(BookService.GetBooks(searchBy,filterBy,orderBy));
                if (BookData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.Found, "Books Found", BookData, ""));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "No Book Found",null, ""));
        }
    }
}
