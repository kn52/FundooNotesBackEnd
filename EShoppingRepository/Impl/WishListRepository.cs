namespace EShoppingRepository.Impl
{
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class WishListRepository : IWishListRepository
    {
        public WishListRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }
        public string AddToWishList(int bookId, string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spAddUserFeedback", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@book_id", bookId);
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));
                    
                    try
                    {
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return "Added To WishList Successfully";
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
            return "Not Added To WishList";
        }
        public List<WishListItems> FetchWishList(string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetBookFeedback", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));

                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            List<WishListItems> wishList = new List<WishListItems>();
                            while (rdr.Read())
                            {
                                wishList.Add(new WishListItems
                                {
                                    wishListItemsId = Convert.ToInt32(rdr["wish_list_items_id"]),
                                    addedToCartDate = (DateTime) rdr["add_to_cart_date"],
                                    bookId = Convert.ToInt32(rdr["book_id"]),
                                    wishListId = Convert.ToInt32(rdr["wish_list_id"]),
                                });
                            }
                            return wishList;
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
