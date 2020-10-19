namespace EShopping.Controllers
{
    using EShoppingModel.Dto;
    using EShoppingModel.Response;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;

    [Route("/bookstore")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public CartController(ICartService service)
        {
            this.CartService = service;
        }

        public ICartService CartService { get; set; }

        [HttpPost]
        [Route("cart")]
        public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
        {
            try
            {
                var CartData = await Task.FromResult(CartService.AddToCart(cartDto));
                if (CartData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Added To Cart", cartDto, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Not Added To Cart", null, ""));
        }
    }
}
