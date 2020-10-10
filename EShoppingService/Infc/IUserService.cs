namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    public interface IUserService
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
        string VerifyUserEmail(string token);
    }
}
