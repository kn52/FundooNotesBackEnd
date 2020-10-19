namespace EShopping.Controllers
{
    using EShoppingModel.Dto;
    using EShoppingModel.Response;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
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
            string CartData;
            try
            {
                CartData = await Task.FromResult(CartService.AddToCart(cartDto));
                if (!CartData.Contains("Not") && CartData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, CartData, cartDto, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, CartData, null, ""));
        }

        [HttpGet]
        [Route("cart")]
        public async Task<IActionResult> fetchCartBook([FromHeader]string token)
        {
            try
            {
                int userId = -1;
                userId= Convert.ToInt32(User.FindFirst("userId").Value);
                if(userId == -1)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                var CartData = await Task.FromResult(CartService.fetchCartBook(userId));
                if (CartData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Books Found", CartData, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Books Not Found", null, ""));
        }
    }
}
