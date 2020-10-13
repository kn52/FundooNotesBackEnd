namespace EShoppingService.Impl
{
    using EShoppingModel.Model;
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
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
        public string AddBook(BookDto bookDto)
        {
            return AdminRepository.AddBook(bookDto);
        }
        public string UpdateBook(BookDto bookDto)
        {
            return AdminRepository.UpdateBook(bookDto);
        }
    }
}
