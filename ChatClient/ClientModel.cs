﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfChatService;

namespace ChatClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ClientModel : IDisposable, IChatServiceCallback
    {
        ChatServiceClient service;

        public Action<Message> NewMessageAction { get; set; }
        public Action<User, Status> NotifyStatusChange { get; set; }

        public User CurrentUser { get; private set; }

        public ClientModel()
        {
            InstanceContext context = new InstanceContext(this);

            service = new ChatServiceClient(context, "NetTcpBinding_IChatService");
        }

        public Response PingServer()
        {
            try
            {
                service.PingService();
            }
            catch
            {
                return Response.Failed;
            }

            return Response.Succes;
        }

        public void Dispose()
        {
            service.Close();
        }

        void IChatServiceCallback.NotifyNewMessage(Message message)
        {
            if(NewMessageAction != null)
            {
                NewMessageAction(message);
            }
        }

        void IChatServiceCallback.NotifyStatusChange(User user, Status newStatus)
        {
            if(NotifyStatusChange != null)
            {
                NotifyStatusChange(user, newStatus);
            }
        }

        public Response Login(string username, string password)
        {
            CurrentUser = new User()
                {
                    Username = username,
                    Password = password
                };

            return service.Login(CurrentUser);
        }

        public void Logout()
        {
            service.Logout(CurrentUser);
        }

        public Response Register(string username, string password)
        {
            return service.Register(new User()
            {
                Username = username,
                Password = password
            });
        }
    
        public User[] GetFriends()
        {
            return service.GetFriends(CurrentUser);
        }

        public Message SendMessage(User to, string message)
        {
            var  msg =new Message()
                {
                    Date = DateTime.Now,
                    Sender = CurrentUser,
                    Receiver = to,
                    MessageText = message,
                    Seen = false
                };

            service.SendMessage(msg);

            return msg;
        }

        public Message[] GetMessages(User partner)
        {
            return service.GetMessages(CurrentUser, partner);
        }

        public void MarkAsRead(Message msg)
        {
            service.MarkAsRead(msg);
        }

        public Response AddFriend(string username)
        {
            if (String.IsNullOrWhiteSpace(username)) return Response.Failed; 

            return service.AddFriend(CurrentUser, new User() { Username = username });
        }

        public Response RemoveFriend(string username)
        {
            if (String.IsNullOrWhiteSpace(username)) return Response.Failed;

            return service.RemoveFriend(CurrentUser, new User() { Username = username });
        }
    }
}
