namespace EShoppingService.Infc
{
    public interface IWishListService
    {
        string AddToWishList(int bookId, int userId);
    }
}
