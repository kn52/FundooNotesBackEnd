namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class OrderService : IOrderService
    {
        public OrderService(IOrderRepository repository,IDistributedCache distributedCache)
        {
            this.OrderRepository = repository;
            this.DistributedCache = distributedCache;
        }
        public IOrderRepository OrderRepository { get; set;}
        public IDistributedCache DistributedCache { get; set; }
        public string PlaceOrder(OrderDto orderDto, string userId)
        {
            if (DistributedCache.GetString("OrderList") != null)
            {
                DistributedCache.Remove("OrderList");
            }
            return OrderRepository.PlaceOrder(orderDto,userId);
        }
        public List<Orders> FetchOrderSummary(string userId)
        {
            List<Orders> books;
            if (DistributedCache.GetString("OrderList") == null)
            {
                books = OrderRepository.FetchOrderSummary(userId);
                DistributedCache.SetString("OrderList", JsonConvert.SerializeObject(books));
                return books;
            }
            books = JsonConvert.DeserializeObject<List<Orders>>(DistributedCache.GetString("OrderList"));
            return books;
         }
    }
}
