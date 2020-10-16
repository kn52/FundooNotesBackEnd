namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface IUserService
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
        string VerifyUserEmail(string token);
        User UserLogin(LoginDto loginDto);
        string ForgetPassword(string email);
        string ResetPassword(ResetPasswordDto resetPasswordDto);
        string GenerateJSONWebToken(int userId);
    }
}
