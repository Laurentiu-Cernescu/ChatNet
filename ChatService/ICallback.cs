using System.ServiceModel;

namespace WcfChatService
{
    interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyNewMessage(Message message);
        [OperationContract(IsOneWay = true)]
        void NotifyStatusChange(User user, Status newStatus);
    }
}
