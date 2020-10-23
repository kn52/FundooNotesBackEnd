namespace EShoppingModel.Util
{
    public class FilterAttribute
    {
        public static string GetAttribute(string attribute)
        {
            if (attribute == null)
            {
                return "";
            }

            string AttributeName = attribute.Trim().ToLower();

            switch (AttributeName)
            {
                case "name":
                    return "book_name";

                case "number":
                    return "no_of_copies";

                case "price":
                    return "book_price";

                case "arrival":
                    return "publishing_year";

                default: 
                    return "";
            }
        }
    }
}
