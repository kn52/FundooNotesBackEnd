namespace EShoppingRepository.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using System.Collections.Generic;

    public interface IOrderRepository
    {
        string PlaceOrder(OrderDto orderDto, string userId);
        List<Orders> FetchOrderSummary(string userId);
    }
}
