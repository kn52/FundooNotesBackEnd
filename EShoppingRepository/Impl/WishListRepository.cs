namespace EShoppingRepository.Impl
{
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
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
        public string AddToWishList(int bookId, int userId)
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

        private readonly string DBString = null;
    }
}
