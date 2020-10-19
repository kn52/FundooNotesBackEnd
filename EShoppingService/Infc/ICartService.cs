namespace EShoppingService.Infc
{
    using EShoppingModel.Dto;
    public interface ICartService
    {
        string AddToCart(CartDto cartDto);
    }
}
