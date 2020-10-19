namespace EShoppingModel.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
    using EShoppingModel.Model;
    using EShoppingModel.Util;
    using EShoppingModel.Util.Infc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    public class UserRepository : IUserRepository
    {
        public UserRepository(IConfiguration configuration,IMessagingService messagingService)
        {
            this.MessagingService = messagingService;
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }

        public IMessagingService MessagingService { get; set; }

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
                    cmd.Parameters.AddWithValue("@registration_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@user_role", userRegistrationDto.userRole);
                    cmd.Parameters.AddWithValue("@key_new", keyNew);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string id = cmd.Parameters["@id"].Value.ToString();
                        if (id != "")
                        {
                            var GeneratedToken = this.GenerateJSONWebToken(Convert.ToInt32(id));
                            MessagingService.Send("Click on below given link to verify your email id " +
                                "<br/> <a href='http://localhost:3000/verify/email/?token=" + GeneratedToken + "'" + ">Verify Email</a>",
                                "ashish52922@gmail.com");
                            return id;
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
            return "";
        }
        public string VerifyUserEmail(string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spVerifyUserEmail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@userId", Convert.ToInt32(userId));
                    cmd.Parameters.AddWithValue("@email_verified", true);
        
                    try
                    {
                        conn.Open();
                        int count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            return "Email Verified";
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
            return "Not Verified";
        }
        public User UserLogin(LoginDto loginDto)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spLogin", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@email", loginDto.email);
                    
                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            User user = new User();
                            while (rdr.Read())
                            {
                                var epass = SaltGenerator.EncodePassword(loginDto.password, rdr["key_new"].ToString());
                                var dpass = SaltGenerator.Base64Decode(rdr["password"].ToString());
                                if (epass.Equals(dpass))
                                {
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
        public string ForgetPassword(string email)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spAdminLogin", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    
                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            this.GenerateJSONWebToken(Convert.ToInt32(rdr["id"]));
                            SendEmail.Email("Reset your password by clicking on below link", email);
                            return "Reset Password Link Is Sent To Your Registered Email";
                        }
                    }
                    catch
                    {
                        return "Reset Password Link Not Sent";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return "Email Not Found";
        }
        public string ResetPassword(ResetPasswordDto resetPasswordDto, string userId)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                var keyNew = SaltGenerator.GeneratePassword(10);
                resetPasswordDto.password = SaltGenerator.Base64Encode(
                    SaltGenerator.EncodePassword(resetPasswordDto.password, keyNew));

                using (SqlCommand cmd = new SqlCommand("spUserResetPassword", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@email", "kns56@gmail.com");
                    cmd.Parameters.AddWithValue("@password", resetPasswordDto.password);
                    cmd.Parameters.AddWithValue("@key_new", keyNew);

                    try
                    {
                        conn.Open();
                        int id = cmd.ExecuteNonQuery();
                        if (id > 0)
                        {
                            return "Reset Password Successfully";
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
            return "Failed To Reset Password";
        }
        public string GenerateJSONWebToken(int userId)
        {
            return TokenGenerator.GenerateJSONWebToken(userId,Configuration);
        }

        private readonly string DBString = null;
    }
}
