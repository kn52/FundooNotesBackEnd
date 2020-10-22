namespace EShoppingRepository.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface IOrderRepository
    {
        string PlaceOrder(OrderDto orderDto, string userId);
        Orders FetchOrderSummary(string userId);
    }
}
