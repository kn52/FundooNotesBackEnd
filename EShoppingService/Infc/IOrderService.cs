namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface IOrderService
    {
        string PlaceOrder(OrderDto orderDto, string userId);
        Orders FetchOrderSummary(string userId);
    }
}
