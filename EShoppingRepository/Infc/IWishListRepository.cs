namespace EShoppingRepository.Infc
{
    public interface IWishListRepository
    {
        string AddToWishList(int bookId, int userId);
    }
}
