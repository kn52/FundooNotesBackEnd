namespace EShoppingRepository.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    public class UserService : IUserService
    {
        public UserService(IUserRepository repository)
        {
            this.UserRepository = repository;
        }
        IUserRepository UserRepository { get; set; }
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
            return UserRepository.UserLogin(loginDto);
        }
        public string ForgetPassword(string email)
        {
            return UserRepository.ForgetPassword(email);
        }
        public string ResetPassword(ResetPasswordDto resetPasswordDto, string userId)
        {
            return UserRepository.ResetPassword(resetPasswordDto, userId);
        }
        public User FetchUserDetail(string userId)
        {
            return UserRepository.FetchUserDetail(userId);
        }
        public string GenerateJSONWebToken(int userId, string userRole)
        {
            return UserRepository.GenerateJSONWebToken(userId,userRole);
        }
    }
}
