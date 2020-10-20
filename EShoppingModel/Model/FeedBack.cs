namespace EShoppingModel.Model
{
    public class FeedBack
    {
        public int feedbackId { get; set; }
        public int rating { get; set; }
        public string feedbackMessage { get; set; }
        public int bookId { get; set; }
        public int userId { get; set; }
    }
}
