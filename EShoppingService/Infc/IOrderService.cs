namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    public interface IOrderService
    {
        string PlaceOrder(OrderDto orderDto, string userId);
    }
}
