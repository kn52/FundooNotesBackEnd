namespace EShoppingRepository.Impl
{
    using EShoppingModel.Model;
    using EShoppingModel.Dto;
    using EShoppingModel.Infc;
    using EShoppingRepository.Infc;
    using Microsoft.Extensions.Caching.Distributed;
    public class AdminService : IAdminService
    {
        public AdminService(IAdminRepository repository, IDistributedCache distributedCache)
        {
            this.AdminRepository = repository;
            this.DistributedCache = distributedCache;
        }
        public IAdminRepository AdminRepository { get; set; }
        public IDistributedCache DistributedCache { get; set; }
        public User AdminLogin(LoginDto loginDto)
        {
            return AdminRepository.AdminLogin(loginDto);
        }
        public string AddBook(BookDto bookDto)
        {
            if (DistributedCache.GetString("BookList") != null)
            {
                DistributedCache.Remove("BookList");
            }
            return AdminRepository.AddBook(bookDto);
        }
        public string UpdateBook(BookDto bookDto)
        {
            if (DistributedCache.GetString("BookList") != null)
            {
                DistributedCache.Remove("BookList");
            }
            return AdminRepository.UpdateBook(bookDto);
        }
        public string DeleteBook(int bookId)
        {
            if (DistributedCache.GetString("BookList") != null)
            {
                DistributedCache.Remove("BookList");
            }
            return AdminRepository.DeleteBook(bookId);
        }
        public string GenerateJSONWebToken(int userId, string userRole)
        {
            return AdminRepository.GenerateJSONWebToken(userId,userRole);
        }
    }
}
