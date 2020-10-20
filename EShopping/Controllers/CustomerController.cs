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
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        public CustomerController(ICustomerService service)
        {
            this.CustomerService = service;
        }
        public ICustomerService CustomerService { get; set; }

        [HttpPost]
        [Route("cart")]
        public async Task<IActionResult> AddToCart([FromBody] CustomerDto customerDto)
        {
            string CustomerData;
            try
            {
                string userId = null;
                userId = User.FindFirst("userId").Value;
                if (userId == null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, "Invalid Token", userId, ""));
                }
                CustomerData = await Task.FromResult(CustomerService.AddCustomerDetails(customerDto, userId));
                if (!CustomerData.Contains("Not") && CustomerData != null)
                {
                    return this.Ok(new ResponseEntity(HttpStatusCode.OK, CustomerData, customerDto, ""));
                }
            }
            catch
            {
                return this.BadRequest(new ResponseEntity(HttpStatusCode.BadRequest, "Bad Request", null, ""));
            }
            return this.Ok(new ResponseEntity(HttpStatusCode.NoContent, CustomerData, null, ""));
        }
    }
}
