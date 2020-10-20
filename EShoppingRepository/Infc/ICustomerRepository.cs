using EShoppingModel.Dto;
using EShoppingModel.Model;

namespace EShoppingRepository.Infc
{
    public interface ICustomerRepository
    {
        string AddCustomerDetails(CustomerDto customerDto, string userId);
        Customer FetchCustomerDetails(int addressType, string userId);
    }
}
