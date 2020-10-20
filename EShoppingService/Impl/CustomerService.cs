namespace EShoppingService.Impl
{
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;

    public class CustomerService : ICustomerService
    {
        public CustomerService(ICustomerRepository repository)
        {
            this.CustomerRepository = repository;
        }
        public ICustomerRepository CustomerRepository { get; set; }
    }
}
