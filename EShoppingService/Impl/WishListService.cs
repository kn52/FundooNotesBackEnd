namespace EShoppingService.Impl
{
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class WishListService : IWishListService
    {
        public WishListService(IWishListRepository repository, IDistributedCache distributedCache)
        {
            this.WishListRepository = repository;
            this.DistributedCache = distributedCache;
        }
        public IWishListRepository WishListRepository { get; set; }
        public IDistributedCache DistributedCache { get; set; }
        public string AddToWishList(int bookId, string userId)
        {
            if (DistributedCache.GetString("WishList") != null)
            {
                DistributedCache.Remove("WishList");
            }
            return WishListRepository.AddToWishList(bookId,userId);
        }
        public List<WishListItems> FetchWishList(string userId)
        {
            List<WishListItems> books;
            if (DistributedCache.GetString("WishList") == null)
            {
                books = WishListRepository.FetchWishList(userId);
                DistributedCache.SetString("WishList", JsonConvert.SerializeObject(books));
                return books;
            }
            books = JsonConvert.DeserializeObject<List<WishListItems>>(DistributedCache.GetString("WishList"));
            return books;
        }
        public string DeleteBookFromWishList(int bookId, string userId)
        {
            if (DistributedCache.GetString("WishList") != null)
            {
                DistributedCache.Remove("WishList");
            }
            return WishListRepository.DeleteBookFromWishList(bookId, userId);
        }
    }
}
