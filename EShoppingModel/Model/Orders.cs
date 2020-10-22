namespace EShoppingModel.Model
{
    using System;
    public class Orders
    {
        public int orderId { get; set; }
        public DateTime orderPlacedDate { get; set; }
        public int totalPrice { get; set; }
        public int userId { get; set; }
        public int customerId { get; set; }
    }
}
