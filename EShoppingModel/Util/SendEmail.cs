namespace EShoppingModel.Util
{
    using EShoppingModel.Util.Infc;
    using Experimental.System.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Text;
    public class SendEmail
    {
        static Message[] messages;
        public SendEmail(IMessagingService messagingService)
        {
            this.MessagingService = messagingService;
            messages = MessagingService.ReceiveMsg();
        }

        public IMessagingService MessagingService { get; set; }

        public static void Email(string htmlString, string to)
        {
            //foreach (Message message in messages)
            //{
            //    message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            //    SendMessage(message.Body.ToString(),to);
                
            //}
            SendMessage(htmlString, to);
        }

        private static void SendMessage(string htmlString,string to)
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
                smtp.Port = 25;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("bookstore.engima@gmail.com", "Engima@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }
    }
}
