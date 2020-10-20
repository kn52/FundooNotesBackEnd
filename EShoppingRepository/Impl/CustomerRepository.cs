namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
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

        private readonly string DBString = null;
    }
}
