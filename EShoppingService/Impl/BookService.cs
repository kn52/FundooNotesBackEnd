namespace EShoppingRepository.Impl
{
    using System.Collections.Generic;
    using EShoppingModel.Model;
    using EShoppingModel.Infc;
    using EShoppingRepository.Infc;
    public class BookService : IBookService
    {
        public BookService(IBookRepository repository)
        {
            this.BookRepository = repository;
        }

        public IBookRepository BookRepository { get; set; }
        public IEnumerable<Book> GetBooks()
        {
            return BookRepository.GetBooks();
        }
    }
}
