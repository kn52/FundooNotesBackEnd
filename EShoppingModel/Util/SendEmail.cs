namespace EShoppingModel.Util
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    public class SendEmail
    {
        public static void Email(string htmlString, string to)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("bookstore.engima@gmail.com");
                message.To.Add(new MailAddress("ashish52922@gmail.com"));
                message.Subject = "Test";
                message.IsBodyHtml = true;  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("bookstore.engima@gmail.com", "Engima@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

            }
            catch (Exception) { }
        }
    }
}
