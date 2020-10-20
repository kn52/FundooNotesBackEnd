namespace EShopping.Controllers
{
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrderService service)
        {
            this.OrderService = service;
        }
        public IOrderService OrderService { get; set; }
    }
}
