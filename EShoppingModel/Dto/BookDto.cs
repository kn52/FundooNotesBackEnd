namespace EShoppingModel.Dto
{
    public class BookDto
    {
        public string isbnNumber { get; set; }
        public string bookName { get; set; }
        public string authorName { get; set; }
        public double bookPrice { get; set; }
        public int noOfCopies { get; set; }
        public string bookDetail { get; set; }
        public string bookImageSrc { get; set; }
        public int publishingYear { get; set; }
    }
}
