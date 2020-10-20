namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;

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
    }
}
