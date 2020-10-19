namespace EShoppingModel.Model
{
    using System;
    public class CartItems
    {
        public int cartItemId { get; set; }
        public DateTime addToCartDate { get; set; }
        public int quantity { get; set; }
        public int bookId { get; set; }
        public int cartId { get; set; }
    }
}
