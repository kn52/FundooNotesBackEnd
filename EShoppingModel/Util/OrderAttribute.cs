namespace EShoppingModel.Util
{
    public class OrderAttribute
    {
        public static string GetAttribute(string attribute)
        {
            if(attribute == null)
            {
                return "";
            }

            string AttributeName = attribute.Trim().ToLower();

            switch (AttributeName)
            {
                case "asc":
                    return "ASC";

                case "desc":
                    return "DESC";

                default:
                    return "";
            }
        }
    }
}
