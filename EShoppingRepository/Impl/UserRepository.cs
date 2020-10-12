namespace EShoppingModel.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
    using EShoppingModel.Model;
    using EShoppingModel.Util;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class UserRepository : IUserRepository
    {
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }

        private readonly IConfiguration Configuration;
        public string UserRegistration(UserRegistrationDto userRegistrationDto)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                var keyNew = SaltGenerator.GeneratePassword(10);
                userRegistrationDto.password = SaltGenerator.Base64Encode(
                    SaltGenerator.EncodePassword(userRegistrationDto.password, keyNew));
                using (SqlCommand cmd = new SqlCommand("spUserRegistration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@email", userRegistrationDto.email);
                    cmd.Parameters.AddWithValue("@email_verified", userRegistrationDto.emailVerified);
                    cmd.Parameters.AddWithValue("@full_name", userRegistrationDto.fullName);
                    cmd.Parameters.AddWithValue("@password", userRegistrationDto.password);
                    cmd.Parameters.AddWithValue("@phone_no", userRegistrationDto.phoneNo);
                    cmd.Parameters.AddWithValue("@user_role", userRegistrationDto.emailVerified);
                    cmd.Parameters.AddWithValue("@key_new", keyNew);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string id = cmd.Parameters["@id"].Value.ToString();
                        if (id != "")
                        {
                            return "Registered Successfully";
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
            return "User Already Exist";
        }
        public string VerifyUserEmail(string token)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spUserRegistration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@userId", 3);
                    cmd.Parameters.AddWithValue("@email_verified", true);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string id = cmd.Parameters["@id"].Value.ToString();
                        if (id != "")
                        {
                            return "User Email Verified";
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
            return "User Email Not Verified";
        }
        public User UserLogin(LoginDto loginDto)
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
                            User user = new User();
                            while (rdr.Read())
                            {
                                var pass = SaltGenerator.Base64Decode(rdr["password"].ToString());
                                Console.WriteLine(pass);
                                user.id = Convert.ToInt32(rdr["id"]);
                                user.fullName = rdr["full_name"].ToString();
                                user.email = rdr["email"].ToString();
                                user.password = rdr["password"].ToString();
                                user.phoneNo = rdr["phone_no"].ToString();
                                user.emailVerified = (bool)rdr["email_verified"];
                                user.userRole = Convert.ToInt32(rdr["user_role"]);
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
        public string GenerateJSONWebToken()
        {
            return TokenGenerator.GenerateJSONWebToken(Configuration);
        }

        public bool ValidateJSONWebToken(string token)
        {
            return TokenGenerator.ValidateJSONWebToken(token, Configuration);
        }

        private readonly string DBString = null;
    }
}
