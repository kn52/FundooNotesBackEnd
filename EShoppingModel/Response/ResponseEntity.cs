namespace EShoppingModel.Response
{
    using System.Net;
    public class ResponseEntity
    {
        public HttpStatusCode httpStatusCode;

        public string message;

        public object data;

        public string token;
        public ResponseEntity(HttpStatusCode httpStatusCode, string message, object data,string token)
        {
            this.httpStatusCode = httpStatusCode;
            this.message = message;
            this.data = message.Contains("Invalid Token") ? "" : data;
            this.token = token;
        }
    }
}
