namespace EShoppingRepository.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface IUserService
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
        string VerifyUserEmail(string userId);
        User UserLogin(LoginDto loginDto);
        string ForgetPassword(string email);
        string ResetPassword(ResetPasswordDto resetPasswordDto, string userId);
        string GenerateJSONWebToken(int userId);
    }
}
