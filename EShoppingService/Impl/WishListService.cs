namespace EShoppingService.Impl
{
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;
    using System.Collections.Generic;

    public class WishListService : IWishListService
    {
        public WishListService(IWishListRepository repository)
        {
            this.WishListRepository = repository;
        }
        public IWishListRepository WishListRepository { get; set; }

        public string AddToWishList(int bookId, string userId)
        {
            return WishListRepository.AddToWishList(bookId,userId);
        }
        public List<WishListItems> FetchWishList(string userId)
        {
            return WishListRepository.FetchWishList(userId);
        }
        public string DeleteBookFromWishList(int bookId, string userId)
        {
            return WishListRepository.DeleteBookFromWishList(bookId, userId);
        }
    }
}
