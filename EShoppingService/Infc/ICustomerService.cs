namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using System.ComponentModel;

    public interface ICustomerService
    {
        string AddCustomerDetails(CustomerDto customerDto,string userId);
        Customer FetchCustomerDetails(int addressType, string userId);
        string AddUserFeedBack(FeedBackDto feedBackDto, string userId);
        FeedBack getBookFeedback(int isbnNumber);
    }
}
