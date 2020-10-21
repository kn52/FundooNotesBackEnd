namespace EShoppingRepository.Infc
{
    using EShoppingModel.Dto;
    public interface IOrderRepository
    {
        string PlaceOrder(OrderDto orderDto, string userId);
    }
}
