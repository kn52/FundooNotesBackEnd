using System.Collections.Generic;

namespace EShoppingService.Infc
{
    public interface IWishListService
    {
        string AddToWishList(int bookId, int userId);
        List<WishList> FetchWishList(string userId);
    }
}
