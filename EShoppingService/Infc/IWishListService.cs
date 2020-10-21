using EShoppingModel.Model;
using System.Collections.Generic;

namespace EShoppingService.Infc
{
    public interface IWishListService
    {
        string AddToWishList(int bookId, string userId);
        List<WishListItems> FetchWishList(string userId);
    }
}
