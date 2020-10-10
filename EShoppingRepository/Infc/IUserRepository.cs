namespace EShoppingModel.Infc
{
    using EShoppingModel.Dto;
    public interface IUserRepository
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
        string VerifyUserEmail(string token);
    }
}
