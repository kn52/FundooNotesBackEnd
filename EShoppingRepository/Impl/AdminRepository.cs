namespace EShoppingRepository.Impl
{
    using EShoppingModel.Model;
    using EShoppingRepository.Dto;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class AdminRepository : IAdminRepository
    {
        public AdminRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }
        public User AdminLogin(LoginDto loginDto)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spAdminLogin", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@email", loginDto.email);
                    cmd.Parameters.AddWithValue("@password", loginDto.password);
                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            User user = null;
                            while (rdr.Read())
                            {
                                user = new User
                                {
                                    id = Convert.ToInt32(rdr["id"]),
                                    fullName = rdr["full_name"].ToString(),
                                    email = rdr["email"].ToString(),
                                    password = rdr["password"].ToString(),
                                    phoneNo = rdr["phone_no"].ToString(),
                                    emailVerified = (bool) rdr["email_verified"],
                                    registrationDate = (DateTime) rdr["registration_date"],
                                    userRole = Convert.ToInt32(rdr["user_role"]),
                                };
                            }
                            return user;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
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
