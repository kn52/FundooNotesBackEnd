﻿namespace EShoppingService.Infc
{
    using EShoppingModel.Model;
    using System.Collections.Generic;
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
    }
}
