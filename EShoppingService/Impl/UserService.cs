namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
    using EShoppingModel.Model;
    using EShoppingService.Infc;
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
        public string VerifyUserEmail(string token)
        {
            return UserRepository.VerifyUserEmail(token);
        }
        public User UserLogin(LoginDto loginDto)
        {
            return UserRepository.UserLogin(loginDto);
        }
        public string ForgetPassword(string email)
        {
            return UserRepository.ForgetPassword(email);
        }
        public string ResetPassword(ResetPasswordDto resetPasswordDto, string token)
        {
            return UserRepository.ResetPassword(resetPasswordDto, token);
        }
        public string GenerateJSONWebToken(int userId)
        {
            return UserRepository.GenerateJSONWebToken(userId);
        }
    }
}
