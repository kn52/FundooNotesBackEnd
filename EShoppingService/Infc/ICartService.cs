namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface ICartService
    {
        string AddToCart(CartDto cartDto,string userId);
        CartItems FetchCartBook(string userId);
        string DeleteFromCartBook(int cartItemId);
        string UpdateCartBookQuantity(int cartItemsId, int quantity);
    }
}