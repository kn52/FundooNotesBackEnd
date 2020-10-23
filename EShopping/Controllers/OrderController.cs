namespace EShopping.Controllers
{
    using EShoppingModel.Dto;
    using EShoppingModel.Response;
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel;
    using System.Net;
    using System.Threading.Tasks;

    [Route("/bookstore")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrderService service)
        {
            this.OrderService = service;
        }
        public IOrderService OrderService { get; set; }

        [HttpPost]
        [Route("/order")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto orderDto)
        {
            string OrderData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                OrderData = await Task.FromResult(OrderService.PlaceOrder(orderDto, userId));
                if (!OrderData.Contains("Not") && OrderData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, OrderData, orderDto, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, OrderData, null, ""));
        }

        [HttpGet]
        [Route("/order")]
        [Description("Fetch Order Summary")]
        public async Task<IActionResult> FetchOrderSummary()
        {
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                var OrderData = await Task.FromResult(OrderService.FetchOrderSummary(userId));
                if (OrderData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Order Found" , OrderData, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, "Order Not Found", null, ""));
        }
    }
}
