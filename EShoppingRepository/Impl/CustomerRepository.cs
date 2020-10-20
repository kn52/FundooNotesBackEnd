namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }
        public string AddCustomerDetails(CustomerDto customerDto, string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spAddUpdateCustomerDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@customer_address", customerDto.customerAddress);
                    cmd.Parameters.AddWithValue("@customer_address_type", customerDto.customerAddressType);
                    cmd.Parameters.AddWithValue("@customer_landmark", customerDto.customerLandmark);
                    cmd.Parameters.AddWithValue("@customer_locality", customerDto.customerLocality);
                    cmd.Parameters.AddWithValue("@customer_pin_code", customerDto.customerPinCode);
                    cmd.Parameters.AddWithValue("@customer_town", customerDto.customerTown);
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));
                    
                    try
                    {
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if(count > 0)
                        {
                            return "Added Customer Detail Successfully";
                        }
                    }
                    catch
                    {
                        return "Customer Detail Not Added";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return "Customer Detail Updated";
        }

        public Customer FetchCustomerDetails(int addressType, string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetCustomerDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@customer_address_type", addressType);
                    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(userId));
                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            Customer customer = new Customer();
                            while (rdr.Read())
                            {
                                customer.customerId = Convert.ToInt32(rdr["customer_id"]);
                                customer.customerAddress = rdr["customer_address"].ToString();
                                customer.customerAddressType = Convert.ToInt32(rdr["customer_address_type"]);
                                customer.customerLandmark = rdr["customer_landmark"].ToString();
                                customer.customerLocality = rdr["customer_locality"].ToString();
                                customer.customerPinCode = Convert.ToInt32(rdr["customer_pin_code"]);
                                customer.customerTown = rdr["customer_town"].ToString();
                                customer.userId = Convert.ToInt32(rdr["user_id"]);
                                return customer;
                            }
                            return null;
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
