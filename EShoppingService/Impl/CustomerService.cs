namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class CustomerService : ICustomerService
    {
        public CustomerService(ICustomerRepository repository, IDistributedCache distributedCache)
        {
            this.CustomerRepository = repository;
            this.DistributedCache = distributedCache;
        }
        public ICustomerRepository CustomerRepository { get; set; }
        public IDistributedCache DistributedCache { get; set; }

        public string AddCustomerDetails(CustomerDto customerDto, string userId)
        {
            if (DistributedCache.GetString("CustomerDetail") != null)
            {
                DistributedCache.Remove("CustomerDetail");
            }
            return CustomerRepository.AddCustomerDetails(customerDto,userId);
        }
        public Customer FetchCustomerDetails(int addressType, string userId)
        {
            Customer customer;
            if (DistributedCache.GetString("CustomerDetail") == null)
            {
                customer = CustomerRepository.FetchCustomerDetails(addressType, userId);
                DistributedCache.SetString("CustomerDetail", JsonConvert.SerializeObject(customer));
                return customer;
            }
            customer = JsonConvert.DeserializeObject<Customer>(DistributedCache.GetString("CustomerDetail"));
            return customer;
        }
        public string AddUserFeedBack(FeedBackDto feedBackDto, string userId)
        {
            if (DistributedCache.GetString("BookFeedBack") != null)
            {
                DistributedCache.Remove("BookFeedBack");
            }
            if (DistributedCache.GetString("UserFeedBack") != null)
            {
                DistributedCache.Remove("UserFeedBack");
            }
            return CustomerRepository.AddUserFeedBack(feedBackDto, userId);        
        }
        public List<FeedBack> getBookFeedback(string isbnNumber)
        {
            List<FeedBack> BookFeedBacks;
            if (DistributedCache.GetString("BookFeedBack") == null)
            {
                BookFeedBacks = CustomerRepository.getBookFeedback(isbnNumber);
                DistributedCache.SetString("BookFeedBack", JsonConvert.SerializeObject(BookFeedBacks));
                return BookFeedBacks;
            }
            BookFeedBacks = JsonConvert.DeserializeObject<List<FeedBack>>(DistributedCache.GetString("BookFeedBack"));
            return BookFeedBacks;
        }
        public FeedBack getUserFeedback(int bookId, string userId)
        {
            FeedBack UserFeedBack;
            if (DistributedCache.GetString("UserFeedBack") == null)
            {
                UserFeedBack = CustomerRepository.getUserFeedback(bookId, userId);
                DistributedCache.SetString("UserFeedBack", JsonConvert.SerializeObject(UserFeedBack));
                return UserFeedBack;
            }
            UserFeedBack = JsonConvert.DeserializeObject<FeedBack>(DistributedCache.GetString("UserFeedBack"));
            return UserFeedBack;
        }
    }
}
