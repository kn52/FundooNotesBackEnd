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
        public string AddBook(BookDto bookDto, string token)
        {
            return AdminRepository.AddBook(bookDto,token);
        }
        public string UpdateBook(BookDto bookDto, string token)
        {
            return AdminRepository.UpdateBook(bookDto,token);
        }
        public string DeleteBook(int bookId, string token)
        {
            return AdminRepository.DeleteBook(bookId,token);
        }
        public string GenerateJSONWebToken(int userId)
        {
            return AdminRepository.GenerateJSONWebToken(userId);
        }
    }
}
