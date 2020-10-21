namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface ICustomerService
    {
        string AddCustomerDetails(CustomerDto customerDto,string userId);
        Customer FetchCustomerDetails(int addressType, string userId);
        string AddUserFeedBack(FeedBackDto feedBackDto, string userId);
        List<FeedBack> getBookFeedback(int isbnNumber);
        FeedBack getUserFeedback(int bookId,string userId);
    }
}
