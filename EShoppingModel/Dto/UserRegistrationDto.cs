namespace EShoppingModel.Dto
{
    public class UserRegistrationDto
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNo { get; set; }
        public bool emailVerified { get; set; }
        public int userRole { get; set; }
    }
}
