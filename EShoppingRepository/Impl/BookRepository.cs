namespace EShoppingModel.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using EShoppingModel.Model;
    using EShoppingModel.Infc;
    using Microsoft.Extensions.Configuration;
    using EShoppingModel.Util;
    public class BookRepository : IBookRepository
    {
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }
        public IEnumerable<Book> GetBooks(string searchBy, string filterBy, string orderBy)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetBooks", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@search",searchBy.Trim().ToLower());
                    cmd.Parameters.AddWithValue("@filter",FilterAttribute.GetAttribute(filterBy));
                    cmd.Parameters.AddWithValue("@order",OrderAttribute.GetAttribute(orderBy));

                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            List<Book> bookList = new List<Book>();
                            while (rdr.Read())
                            {
                                bookList.Add(new Book
                                {
                                    id = Convert.ToInt32(rdr["id"]),
                                    authorName = rdr["auther_name"].ToString(),
                                    bookDetail = rdr["book_detail"].ToString(),
                                    bookImageSrc = rdr["book_image_src"].ToString(),
                                    bookName = rdr["book_name"].ToString(),
                                    bookPrice = Convert.ToInt32(rdr["book_price"]),
                                    isbnNumber = rdr["isbn_number"].ToString(),
                                    noOfCopies = Convert.ToInt32(rdr["no_of_copies"]),
                                    publishingYear = Convert.ToInt32(rdr["publishing_year"])
                                });
                            }
                            return bookList;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        private readonly string DBString = null;
    }
}
