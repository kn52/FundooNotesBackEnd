﻿namespace EShoppingService.Impl
{
    using EShoppingModel.Dto;
    using EShoppingModel.Model;
    using EShoppingRepository.Infc;
    using EShoppingService.Infc;
    public class CartService : ICartService
    {
        public CartService(ICartRepository repository)
        {
            this.CartRepository = repository;
        }
        public ICartRepository CartRepository { get; set; }
        public string AddToCart(CartDto cartDto, string userId)
        {
            return CartRepository.AddToCart(cartDto,userId);
        }
        public CartItems FetchCartBook(string userId)
        {
            return CartRepository.FetchCartBook(userId);
        }
        public string DeleteFromCartBook(int cartItemId)
        {
            return CartRepository.DeleteFromCartBook(cartItemId);
        }

        public string UpdateCartBookQuantity(int cartItemsId, int quantity)
        {
            throw new System.NotImplementedException();
        }
    }
}
