namespace EShoppingRepository.Infc
{
    using EShoppingModel.Model;
    using System.Collections.Generic;
    public interface IBookService
    {
        IEnumerable<Book> GetBooks(string searchBy,string filterBy,string orderBy);
    }
}