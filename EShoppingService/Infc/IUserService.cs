namespace EShoppingService.Infc
{
    using EShoppingRepository.Dto;
    public interface IUserService
    {
        string UserRegistration(UserRegistrationDto userRegistrationDto);
    }
}
