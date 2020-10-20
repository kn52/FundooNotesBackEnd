using EShoppingModel.Dto;

namespace EShoppingRepository.Infc
{
    public interface ICustomerRepository
    {
        string AddCustomerDetails(CustomerDto customerDto, string userId);
    }
}
