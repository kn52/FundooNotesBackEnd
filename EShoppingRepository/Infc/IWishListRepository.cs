namespace EShoppingRepository.Infc
{
    using EShoppingModel.Model;
    using System.Collections.Generic;
    public interface IWishListRepository
    {
        string AddToWishList(int bookId, string userId);
        List<WishListItems> FetchWishList(string userId);
        string DeleteBookFromWishList(int bookId, string userId);
    }
}
