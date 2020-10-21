namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;

    public class OrderService : IOrderService
    {
        public OrderService(IOrderRepository repository)
        {
            this.OrderRepository = repository;
        }
        public IOrderRepository OrderRepository { get; set;}

        public string PlaceOrder(OrderDto orderDto, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
