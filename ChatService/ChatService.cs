using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WcfChatService
{
    public class ChatService : IChatService
    {
        ChatNetEntities context;

        Dictionary<int, UserData> loggedUsers;

        public ChatService()
        {
            context = new ChatNetEntities();

            loggedUsers = new Dictionary<int, UserData>();
        }

        public Response Login(User user)
        {
            var dbUser = (from usr in context.Users
                          where usr.Username == user.Username
                          select usr).FirstOrDefault();

            if (dbUser == null || !dbUser.Password.Equals(user.Password)) return Response.Failed;

            if(!loggedUsers.ContainsKey(dbUser.Id))
            {
                dbUser.Status = Status.Online;

                ICallback callback = OperationContext.Current.GetCallbackChannel<ICallback>();

                loggedUsers.Add(dbUser.Id, new UserData()
                    {
                        User = dbUser,
                        Callback = callback
                    });

                foreach(var friend in GetFriends(dbUser))
                {
                    if(friend.Status == Status.Online)
                    {
                        loggedUsers[friend.Id].Callback.NotifyStatusChange(dbUser, Status.Online);
                    }
                }
            }

            return Response.Succes;
        }

        public Response Logout(User user)//testing required
        {
            var userDataKey = loggedUsers.Where(x => x.Value.User.Username.Equals(user.Username)).FirstOrDefault();

            if (userDataKey.Value != null)
            {
                foreach (var friend in GetFriends(userDataKey.Value.User))
                {
                    if(friend.Status == Status.Online)
                    {
                        loggedUsers[friend.Id].Callback.NotifyStatusChange(userDataKey.Value.User, Status.Offline);
                    }
                }
            }

            return Response.Succes;
        }

        public Response SendMessage(Message message)
        {
            UserData receiver = null;

            var dbReceiver = (from usr in context.Users
                              where usr.Username == message.Receiver.Username
                              select usr).FirstOrDefault();

            context.Messages.Add(message);

            context.SaveChanges();
           
            if(loggedUsers.TryGetValue(dbReceiver.Id, out receiver))
            {
                receiver.Callback.NotifyNewMessage(message);
            }
            
            return Response.Succes;
        }

        public User[] GetFriends(User user)
        {
            var dbUser = (from usr in context.Users
                          where usr.Username == user.Username
                          select usr).FirstOrDefault();

            List<User> partB = (from link in context.FriendshipLinks
                                where link.PartA.Id == dbUser.Id
                                select CheckStatus(link.PartB)) as List<User>;

            List<User> partA = (from link in context.FriendshipLinks
                                where link.PartB.Id == dbUser.Id
                                select CheckStatus(link.PartA)) as List<User>;

            partB.ForEach(x => partA.Add(x));

            return partA.ToArray();
        }

        public Message[] GetMessages(User owner, User partner)
        {
            var dbUserOwner = (from usr in context.Users
                               where usr.Username == owner.Username
                               select usr).FirstOrDefault();

            var dbUserPartner = (from usr in context.Users
                                 where usr.Username == partner.Username
                                 select usr).FirstOrDefault();

            List<Message> messages = (from msg in context.Messages
                                      where msg.Sender.Id == dbUserOwner.Id && msg.Receiver.Id == dbUserPartner.Id || 
                                            msg.Sender.Id == dbUserPartner.Id && msg.Receiver.Id == dbUserOwner.Id
                                      select msg) as List<Message>;

            return messages.ToArray();
        }

        public Response AddFriend(User fromUser, User toUser)
        {
            var uFrom =  (from usr in context.Users
                               where usr.Username == fromUser.Username
                               select usr).FirstOrDefault();

            var uTo =  (from usr in context.Users
                               where usr.Username == toUser.Username
                               select usr).FirstOrDefault();

            var existing = (from link in context.FriendshipLinks
                            where   link.PartA.Id == uFrom.Id && link.PartB.Id == uTo.Id ||
                                    link.PartA.Id == uTo.Id && link.PartB.Id == uFrom.Id
                            select link).FirstOrDefault();

            if(existing == null)
            {
                context.FriendshipLinks.Add(new FriendshipLink()
                    {
                        PartA = uFrom,
                        PartB = uTo
                    });

                context.SaveChanges();
            }

            return Response.Succes;
        }

        public Response RemoveFriend(User fromUser, User toUser)
        {
            var uFrom = (from usr in context.Users
                         where usr.Username == fromUser.Username
                         select usr).FirstOrDefault();

            var uTo = (from usr in context.Users
                       where usr.Username == toUser.Username
                       select usr).FirstOrDefault();

            var existing = (from link in context.FriendshipLinks
                            where link.PartA.Id == uFrom.Id && link.PartB.Id == uTo.Id ||
                                    link.PartA.Id == uTo.Id && link.PartB.Id == uFrom.Id
                            select link).FirstOrDefault();

            if (existing != null)
            {
                context.FriendshipLinks.Remove(existing);

                context.SaveChanges();
            }

            return Response.Succes;
        }

        public Response MarkAsRead(Message message)
        {
            var dbMessage = (from msg in context.Messages
                             where msg.Id == message.Id
                             select msg).FirstOrDefault();

            if(dbMessage != null)
            {
                dbMessage.Seen = true;
                context.SaveChanges();
            }

            return Response.Succes;
        }

        public int PingService()
        {
            Console.WriteLine("pinged !!!");

            ICallback callback = OperationContext.Current.GetCallbackChannel<ICallback>();

            if (((ICommunicationObject)callback).State == CommunicationState.Opened)
            {
                //callback.NotifyStatusChange(null, Status.Online);
            }

            return 0;
        }

        User CheckStatus(User user)
        {
            if(user.Id == 0) throw new Exception("User id is 0");

            user.Status = loggedUsers.ContainsKey(user.Id) ? Status.Online : Status.Offline;

            return user;
        }
    }
}
