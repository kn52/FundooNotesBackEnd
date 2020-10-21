namespace EShoppingModel.Model
{
    using System;
    public class WishListItems
    {
        public int wishListItemsId { get; set; }
        public DateTime addedToCartDate { get; set; }
        public int bookId { get; set; }
        public int wishListId { get; set; }
    }
}
