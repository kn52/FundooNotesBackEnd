namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Util;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
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
        public string AddToCart(CartDto cartDto)
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
                    cmd.Parameters.AddWithValue("@user_id", 7);

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
        private int ValidateJSONWebToken(string token)
        {
            return TokenGenerator.ValidateJSONWebToken(token, Configuration);
        }

        private readonly string DBString = null;
    }
}
