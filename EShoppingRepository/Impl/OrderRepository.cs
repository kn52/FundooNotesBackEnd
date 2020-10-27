namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingModel.Util.Infc;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
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
                            using (SqlCommand cmd0 = new SqlCommand("spFetchUserDetail",conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            }){
                                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));
                                try
                                {
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
                                    return "Order Details Not Sent To Your Registered Email Id";
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

        public List<Orders> FetchOrderSummary(string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spFetchUserDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            List<Orders> orders = new List<Orders>();
                            while (rdr.Read())
                            {
                                orders.Add(new Orders {
                                    orderId = Convert.ToInt32(rdr["order_id"]),
                                    orderPlacedDate = (DateTime) rdr["order_placed_date"],
                                    totalPrice = Convert.ToInt32(rdr["total_price"]),
                                    customerId = Convert.ToInt32(rdr["customer_id"]),
                                    userId = Convert.ToInt32(rdr["user_id"])
                                });
                            }
                            return orders;
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
