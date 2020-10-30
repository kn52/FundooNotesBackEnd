namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    public class CartService : ICartService
    {
        public CartService(ICartRepository repository, IDistributedCache distributedCache)
        {
            this.CartRepository = repository;
            this.DistributedCache = distributedCache;
        }
        public ICartRepository CartRepository { get; set; }
        public IDistributedCache DistributedCache { get; set; }
        public string AddToCart(CartDto cartDto, string userId)
        {
            return CartRepository.AddToCart(cartDto,userId);
        }
        public CartItems FetchCartBook(string userId)
        {
            return CartRepository.FetchCartBook(userId);
        }
        public string DeleteFromCartBook(int cartItemId)
        {
            return CartRepository.DeleteFromCartBook(cartItemId);
        }

        public string UpdateCartBookQuantity(int cartItemsId, int quantity)
        {
            return CartRepository.UpdateCartBookQuantity(cartItemsId,quantity);
        }
    }
}
