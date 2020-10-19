using EShoppingModel.Dto;

namespace EShoppingRepository.Infc
{
    public interface ICartRepository
    {
        string AddToCart(CartDto cartDto);
    }
}
