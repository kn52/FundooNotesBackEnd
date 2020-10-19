namespace EShoppingRepository.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface ICartRepository
    {
        string AddToCart(CartDto cartDto);
        CartItems fetchCart();
    }
}
