namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
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
            throw new System.NotImplementedException();
        }
    }
}
