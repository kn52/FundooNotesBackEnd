namespace EShoppingService.Impl
{
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;
    public class WishListService : IWishListService
    {
        public WishListService(IWishListRepository repository)
        {
            this.WishListRepository = repository;
        }
        public IWishListRepository WishListRepository { get; set; }

        public string AddToWishList(int bookId, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
