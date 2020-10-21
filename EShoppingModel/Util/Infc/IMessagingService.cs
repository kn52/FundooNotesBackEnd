namespace EShoppingModel.Util.Infc
{
    using Experimental.System.Messaging;
    public interface IMessagingService
    {
        void Send(string message, string v);
        Message[] ReceiveMsg();
    }
}
