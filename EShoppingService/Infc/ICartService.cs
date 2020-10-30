namespace EShoppingRepository.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using System.Collections.Generic;

    public interface ICartService
    {
        string AddToCart(CartDto cartDto,string userId);
        IEnumerable<CartItems> FetchCartBook(string userId);
        string DeleteFromCartBook(int cartItemId);
        string UpdateCartBookQuantity(int cartItemsId, int quantity);
    }
}