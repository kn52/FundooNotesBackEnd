namespace EShoppingModel.Infc
{
    using EShoppingModel.Model;
    using System.Collections.Generic;
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
    }
}
