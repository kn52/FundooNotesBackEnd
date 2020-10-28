namespace EShoppingRepository.Impl
{
    using EShoppingModel.Model;
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
    using EShoppingRepository.Infc;
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
        public string DeleteBook(int bookId)
        {
            return AdminRepository.DeleteBook(bookId);
        }
        public string GenerateJSONWebToken(int userId, string userRole)
        {
            return AdminRepository.GenerateJSONWebToken(userId,userRole);
        }
    }
}
