using EShoppingModel.Dto;
using EShoppingModel.Model;
using System.Collections.Generic;

namespace EShoppingRepository.Infc
{
    public interface ICustomerRepository
    {
        string AddCustomerDetails(CustomerDto customerDto, string userId);
        Customer FetchCustomerDetails(int addressType, string userId);
        string AddUserFeedBack(FeedBackDto feedBackDto, string userId);
        List<FeedBack> getBookFeedback(int isbnNumber);
    }
}
