namespace EShoppingModel.Model
{
    using System;
    public class User
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNo { get; set; }
        public bool emailVerified { get; set; }
        public DateTime registrationDate { get; set; }
        public int userRole { get; set; }
    }
}
