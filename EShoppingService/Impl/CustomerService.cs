namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using EShoppingRepository.Infc;
    using System.Collections.Generic;

    public class CustomerService : ICustomerService
    {
        public CustomerService(ICustomerRepository repository)
        {
            this.CustomerRepository = repository;
        }
        public ICustomerRepository CustomerRepository { get; set; }

        public string AddCustomerDetails(CustomerDto customerDto, string userId)
        {
            return CustomerRepository.AddCustomerDetails(customerDto,userId);
        }
        public Customer FetchCustomerDetails(int addressType, string userId)
        {
            return CustomerRepository.FetchCustomerDetails(addressType, userId);
        }
        public string AddUserFeedBack(FeedBackDto feedBackDto, string userId)
        {
            return CustomerRepository.AddUserFeedBack(feedBackDto, userId);        
        }
        public List<FeedBack> getBookFeedback(string isbnNumber)
        {
            return CustomerRepository.getBookFeedback(isbnNumber);
        }
        public FeedBack getUserFeedback(int bookId, string userId)
        {
            return CustomerRepository.getUserFeedback(bookId,userId);
        }
    }
}
