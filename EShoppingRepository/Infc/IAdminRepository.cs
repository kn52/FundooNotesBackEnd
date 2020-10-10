namespace EShoppingRepository.Infc
{
    using EShoppingModel.Model;
    using EShoppingRepository.Dto;
    public interface IAdminRepository
    {
        User AdminLogin(LoginDto loginDto);
    }
}
