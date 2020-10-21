namespace EShoppingModel.Model
{
    public class Customer
    {
        public int customerId { get; set; }
        public int customerPinCode { get; set; }
        public string customerLocality { get; set; }
        public string customerAddress { get; set; }
        public string customerTown { get; set; }
        public string customerLandmark { get; set; }
        public int customerAddressType { get; set; }
        public int userId { get; set; }
    }
}
