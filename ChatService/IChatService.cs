﻿using System.ServiceModel;

namespace WcfChatService
{
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IChatService
    {
        [OperationContract]
        Response Login(User user);
        [OperationContract]
        Response Logout(User user);
        [OperationContract]
        Response Register(User user);
        [OperationContract]
        Response SendMessage(Message message);
        [OperationContract]
        User[] GetFriends(User user);
        [OperationContract]
        Message[] GetMessages(User owner, User partner);
        [OperationContract]
        Response AddFriend(User from, User to);
        [OperationContract]
        Response RemoveFriend(User from, User to);
        [OperationContract]
        Response MarkAsRead(Message message);
        [OperationContract]
        int PingService();
    }
}
