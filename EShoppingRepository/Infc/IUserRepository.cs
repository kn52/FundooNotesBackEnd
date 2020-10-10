namespace EShoppingRepository.Infc
{
    using EShoppingRepository.Dto;
    public interface IUserRepository
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
    }
}
