namespace EShoppingModel.Infc
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;

    public interface IUserRepository
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
        string VerifyUserEmail(string userId);
        User UserLogin(LoginDto loginDto);
        string ForgetPassword(string email);
        string ResetPassword(ResetPasswordDto resetPasswordDto, string userId);
        User FetchUserDetail(string userId);
        string GenerateJSONWebToken(int userId, string userRole);
    }
}
