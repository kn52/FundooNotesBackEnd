namespace EShoppingService.Impl
{
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;

    public class OrderService : IOrderService
    {
        public OrderService(IOrderRepository repository)
        {
            this.OrderRepository = repository;
        }
        public IOrderRepository OrderRepository { get; set;}
    }
}
