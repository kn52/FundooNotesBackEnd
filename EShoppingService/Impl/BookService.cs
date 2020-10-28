namespace EShoppingRepository.Impl
{
    using System.Collections.Generic;
    using EShoppingModel.Model;
    using EShoppingModel.Infc;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    public class BookService : IBookService
    {
        public BookService(IBookRepository repository, IDistributedCache distributedCache)
        {
            this.BookRepository = repository;
            this.DistributedCache = distributedCache;

        }
        public IBookRepository BookRepository { get; set; }
        public IDistributedCache DistributedCache { get; set; }
        public IEnumerable<Book> GetBooks(string searchBy, string filterBy, string orderBy)
        {
            IEnumerable<Book> books;
            if (DistributedCache.GetString("BookList") == null)
            {
                books = BookRepository.GetBooks(searchBy, filterBy, orderBy);
                DistributedCache.SetString("BookList", JsonConvert.SerializeObject(books));
                return books;
            }
            books = JsonConvert.DeserializeObject<IEnumerable<Book>>(DistributedCache.GetString("BookList"));
            return books;
        }
    }
}
