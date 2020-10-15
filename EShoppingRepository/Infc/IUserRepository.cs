namespace EShoppingModel.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface IUserRepository
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
        string VerifyUserEmail(string token);
        User UserLogin(LoginDto loginDto);
        string ResetPassword(ResetPasswordDto resetPasswordDto);
        string GenerateJSONWebToken(int userId);
        int ValidateJSONWebToken(string token);
    }
}
