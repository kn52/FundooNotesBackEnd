namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingModel.Util;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    
    public class CartRepository : ICartRepository
    {
        public CartRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"]; 
        }
        public IConfiguration Configuration { get; set; }
        public string AddToCart(CartDto cartDto,string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spAddToCart", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@quantity", cartDto.quantity);
                    cmd.Parameters.AddWithValue("@book_id", cartDto.bookId);
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));

                    try
                    {
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return "Added To Cart Successfully";
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
            return "Not Added To Cart";
        }

        public string GenerateJSONWebToken(int userId)
        {
            return TokenGenerator.GenerateJSONWebToken(userId, Configuration);
        }
        public CartItems FetchCartBook(string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spFetchCartBook", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@user_id",Convert.ToInt32(userId));

                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            CartItems cartItems = new CartItems();
                            while (rdr.Read())
                            {
                                    cartItems.cartItemId = Convert.ToInt32(rdr["cart_items_id"]);
                                    cartItems.addToCartDate = (DateTime)rdr["add_to_cart_date"];
                                    cartItems.quantity = Convert.ToInt32(rdr["quantity"]) ;
                                    cartItems.bookId = Convert.ToInt32(rdr["book_id"]);
                                    cartItems.cartId = Convert.ToInt32(rdr["cart_id"]);
                            }
                            return cartItems;
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

        public string DeleteFromCartBook(int cartItemId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spDeleteBookFromCart", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@cart_items_id", cartItemId);

                    try
                    {
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return "Book Deleted Successfully";
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
            return "Cart Items Id Not Found";
        }

        public string UpdateCartBookQuantity(int cartItemsId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateCartBookQuantity", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@cart_items_id", cartItemsId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    
                    try
                    {
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return "Book Quantity Updated Successfully";
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
            return "Cart Items Id Not Found";
        }

        private readonly string DBString = null;
    }
}
