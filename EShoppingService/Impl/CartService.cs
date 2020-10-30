namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class CartService : ICartService
    {
        public CartService(ICartRepository repository, IDistributedCache distributedCache)
        {
            this.CartRepository = repository;
        }
        public ICartRepository CartRepository { get; set; }
        public IDistributedCache DistributedCache { get; set; }
        public string AddToCart(CartDto cartDto, string userId)
        {
            if (DistributedCache.GetString("CartList") != null)
            {
                DistributedCache.Remove("CartList");
            }
            return CartRepository.AddToCart(cartDto,userId);
        }
        public IEnumerable<CartItems> FetchCartBook(string userId)
        {
            IEnumerable<CartItems> books;
            if (DistributedCache.GetString("CartList") == null)
            {
                books = CartRepository.FetchCartBook(userId);
                DistributedCache.SetString("CartList", JsonConvert.SerializeObject(books));
                return books;
            }
            books = JsonConvert.DeserializeObject<IEnumerable<CartItems>>(DistributedCache.GetString("CartList"));
            return books;
        }
        public string DeleteFromCartBook(int cartItemId)
        {
            if (DistributedCache.GetString("CartList") != null)
            {
                DistributedCache.Remove("CartList");
            }
            return CartRepository.DeleteFromCartBook(cartItemId);
        }
        public string UpdateCartBookQuantity(int cartItemsId, int quantity)
        {
            if (DistributedCache.GetString("CartList") != null)
            {
                DistributedCache.Remove("CartList");
            }
            return CartRepository.UpdateCartBookQuantity(cartItemsId,quantity);
        }
    }
}
