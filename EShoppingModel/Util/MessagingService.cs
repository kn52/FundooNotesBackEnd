namespace EShoppingModel.Util
{
    using EShoppingModel.Util.Infc;
    using Experimental.System.Messaging;
    using System;
    public class MessagingService : IMessagingService
    {
        private MessageQueue messagQueue = new MessageQueue();
        public MessagingService()
        {
            this.messagQueue.Path = @".\private$\emailsender";
            if (!MessageQueue.Exists(this.messagQueue.Path))
            {
                this.messagQueue = MessageQueue.Create(this.messagQueue.Path);
            }
        }

        public void Send(string message,string to)
        {
            this.messagQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            this.messagQueue.Send(message);
            SendEmail.Email();
        }

        public Message[] ReceiveMsg()
        {
            return messagQueue.GetAllMessages();
        }
    } 
}
