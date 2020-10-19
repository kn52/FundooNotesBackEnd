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
        public static void Email(string htmlString, string to)
        {
            SendMessage(htmlString, to);
        }

        private static void SendMessage(string htmlString,string to)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress("countrybookshop@gmail.com");
                mailMessage.Subject = "hsdjfd";
                mailMessage.Body = "bshdjkv";
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress("ashish52922@gmail.com"));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";

                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = "RuntimeTerror@123";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }
    }
}
