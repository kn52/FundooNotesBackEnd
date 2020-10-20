namespace EShopping.Controllers
{
    using EShoppingService.Infc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    [Route("/bookstore")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        public CustomerController(ICustomerService service)
        {
            this.CustomerService = service;
        }
        public ICustomerService CustomerService { get; set; }
    }
}
