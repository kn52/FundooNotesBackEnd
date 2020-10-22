namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Util.Infc;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(IConfiguration configuration,IMessagingService messagingService)
        {
            this.MessagingService = messagingService;
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }

        public IMessagingService MessagingService { get; set; }

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
                            using (SqlCommand cmd0 = new SqlCommand("spFetchUser",conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            }){
                                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));
                                try
                                {
                                    conn.Open();
                                    SqlDataReader rdr = cmd.ExecuteReader();
                                    if (rdr.HasRows)
                                    {
                                        while (rdr.Read())
                                        {
                                            string email = rdr["email"].ToString();
                                            MessagingService.Send("Your order placed successfully with order id " +
                                                 "OrdedId","ashish52922@gmail.com");
                                            break;
                                        }
                                    }
                                }
                                catch 
                                {
                                    return "Detail Of Order Not Sent On Your Registered Email Id";
                                }
                            }
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
