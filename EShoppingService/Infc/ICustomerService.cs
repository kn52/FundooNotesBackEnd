namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    public interface ICustomerService
    {
        string AddCustomerDetails(CustomerDto customerDto,string userId);
    }
}
