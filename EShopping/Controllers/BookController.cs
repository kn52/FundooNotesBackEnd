namespace EShopping.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using EShoppingModel.Response;
    using EShoppingRepository.Infc;
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
        [Route("books/{searchBy}/{filterBy}/{orderBy}")]
        public async Task<IActionResult> GetBook(string searchBy,string filterBy,string orderBy)
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
