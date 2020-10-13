namespace EShoppingModel.Infc
{
    using EShoppingModel.Model;
    using EShoppingModel.Dto;
    public interface IAdminRepository
    {
        User AdminLogin(LoginDto loginDto);
        string AddBook(BookDto bookDto);
        string UpdateBook(BookDto bookDto);
        string GenerateJSONWebToken(User user);
    }
}
