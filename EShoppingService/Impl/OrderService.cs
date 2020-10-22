﻿namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
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
            return OrderRepository.PlaceOrder(orderDto,userId);
        }
        public Orders FetchOrderSummary(string userId)
        {
            return FetchOrderSummary(userId);
        }
    }
}
