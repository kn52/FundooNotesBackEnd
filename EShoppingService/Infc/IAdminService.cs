namespace EShoppingService.Infc
{
    using EShoppingModel.Model;
    using EShoppingRepository.Dto;
    public interface IAdminService
    {
        User AdminLogin(LoginDto loginDto);
    }
}
