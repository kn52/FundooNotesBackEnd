namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    public class UserService : IUserService
    {
        public UserService(IUserRepository repository, IDistributedCache distributedCache)
        {
            this.UserRepository = repository;
            this.DistributedCache = distributedCache;
        }
        IUserRepository UserRepository { get; set; }
        public IDistributedCache DistributedCache { get; set; }
        public string UserRegistration(UserRegistrationDto userRegistrationDto)
        {
            return UserRepository.UserRegistration(userRegistrationDto);
        }
        public string VerifyUserEmail(string userId)
        {
            return UserRepository.VerifyUserEmail(userId);
        }
        public User UserLogin(LoginDto loginDto)
        {
            if (DistributedCache.GetString("User") != null)
            {
                DistributedCache.Remove("User");
            }
            return UserRepository.UserLogin(loginDto);
        }
        public string ForgetPassword(string email)
        {
            return UserRepository.ForgetPassword(email);
        }
        public string ResetPassword(ResetPasswordDto resetPasswordDto, string userId)
        {
            if (DistributedCache.GetString("User") != null)
            {
                DistributedCache.Remove("User");
            }
            return UserRepository.ResetPassword(resetPasswordDto, userId);
        }
        public User FetchUserDetail(string userId)
        {
            User user;
            if (DistributedCache.GetString("User") == null)
            {
                user = UserRepository.FetchUserDetail(userId);
                DistributedCache.SetString("User", JsonConvert.SerializeObject(user));
                return user;
            }
            user = JsonConvert.DeserializeObject<User>(DistributedCache.GetString("User"));
            return user;
        }
        public string GenerateJSONWebToken(int userId, string userRole)
        {
            return UserRepository.GenerateJSONWebToken(userId,userRole);
        }
    }
}
