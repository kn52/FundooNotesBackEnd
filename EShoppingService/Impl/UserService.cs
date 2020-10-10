namespace EShoppingService.Impl
{
    using EShoppingRepository.Dto;
    using EShoppingRepository.Infc;
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
    }
}
