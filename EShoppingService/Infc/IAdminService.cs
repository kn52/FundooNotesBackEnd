namespace EShoppingService.Infc
{
    using EShoppingModel.Model;
    using EShoppingModel.Dto;
    public interface IAdminService
    {
        User AdminLogin(LoginDto loginDto);
        string AddBook(BookDto bookDto);
        string UpdateBook(BookDto bookDto);
        string DeleteBook(int bookId);
        string GenerateJSONWebToken(User user);
    }
}
