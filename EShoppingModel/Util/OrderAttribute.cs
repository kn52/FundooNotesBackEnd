namespace EShoppingModel.Util
{
    public class OrderAttribute
    {
        public static string GetAttribute(string attribute)
        {
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
