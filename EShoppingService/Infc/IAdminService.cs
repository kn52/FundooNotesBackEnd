namespace EShoppingService.Infc
{
    using EShoppingModel.Model;
    using EShoppingModel.Dto;
    public interface IAdminService
    {
        User AdminLogin(LoginDto loginDto);
    }
}
