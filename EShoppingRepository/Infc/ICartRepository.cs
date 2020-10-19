namespace EShoppingRepository.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface ICartRepository
    {
        string AddToCart(CartDto cartDto, string userId);
        CartItems FetchCartBook(string userId);
        string DeleteFromCartBook(int cartItemId);
    }
}
