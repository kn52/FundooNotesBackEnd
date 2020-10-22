namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using System.Collections.Generic;

    public interface IOrderService
    {
        string PlaceOrder(OrderDto orderDto, string userId);
        List<Orders> FetchOrderSummary(string userId);
    }
}
