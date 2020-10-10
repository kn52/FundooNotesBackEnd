namespace EShoppingService.Impl
{
    using EShoppingModel.Model;
    using EShoppingRepository.Dto;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;
    public class AdminService : IAdminService
    {
        public AdminService(IAdminRepository repository)
        {
            this.AdminRepository = repository;
        }
        public IAdminRepository AdminRepository { get; set; }
        public User AdminLogin(LoginDto loginDto)
        {
            return AdminRepository.AdminLogin(loginDto);
        }
    }
}
