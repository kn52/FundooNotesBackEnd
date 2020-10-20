namespace EShopping.Controllers
{
    using EShoppingModel.Response;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;

    [Route("/bookstore")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        public WishListController(IWishListService service)
        {
            this.WishListService = service;
        }
        public IWishListService WishListService { get; set; }

        [HttpPost]
        [Route("wishlist/{bookid}")]
        public async Task<IActionResult> AddCustomerDetails(int bookId)
        {
            string WishListData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                WishListData = await Task.FromResult(WishListService.AddToWishList(bookId, userId));
                if (!WishListData.Contains("Not") && WishListData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, WishListData, bookId, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Not Added", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, WishListData, null, ""));
        }
    }
}
