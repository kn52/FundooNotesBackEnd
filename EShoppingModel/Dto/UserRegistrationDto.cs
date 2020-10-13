namespace EShoppingModel.Dto
{
    using System.ComponentModel;
    public class UserRegistrationDto
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNo { get; set; }

        [DefaultValue(false)]
        public bool emailVerified { get; set; }

        [DefaultValue(1)]
        public int userRole { get; set; } = 1;
    }
}
