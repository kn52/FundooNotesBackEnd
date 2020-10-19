namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;
    public class CartService : ICartService
    {
        public CartService(ICartRepository repository)
        {
            this.CartRepository = repository;
        }
        public ICartRepository CartRepository { get; set; }
        public string AddToCart(CartDto cartDto)
        {
            return CartRepository.AddToCart(cartDto);
        }
    }
}
