namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }

        public string PlaceOrder(OrderDto orderDto, string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spPlaceOrder", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@order_id", "000394");
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));

                    try
                    {
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return "Order Placed Successfully";
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
            return "Order Not Placed";
        }

        private readonly string DBString = null;        
    }
}
