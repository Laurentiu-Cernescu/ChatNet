using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WcfChatService
{
    public class ChatService : IChatService
    {
        public static void TestEntityFramework()
        {
            using(var context = new ChatNetEntities())
            {
                Console.WriteLine("Registered users: " + context.Users.Count());
                Console.WriteLine("friendships: " + context.FriendshipLinks.Count());
                Console.WriteLine("Messages sent: " + context.Messages.Count());
            }
        }

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
                          where usr.Username.Trim() == user.Username.Trim()
                          select usr).FirstOrDefault();

            if (dbUser == null || !dbUser.Password.Trim().Equals(user.Password.Trim())) return Response.Failed;

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
            var userDataKey = loggedUsers.Where(x => x.Value.User.Username.Trim().Equals(user.Username.Trim())).FirstOrDefault();

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

        public Response Register(User user)
        {
            var dbUser = (from usr in context.Users
                          where usr.Username.Trim() == user.Username.Trim()
                          select usr).FirstOrDefault();
            if (dbUser == null)
            {
                context.Users.Add(new User()
                    {
                        Username = user.Username.Trim(),
                        Password = user.Password.Trim(),
                    });

                context.SaveChanges();

                return Response.Succes;
            }

            return Response.Failed;
        }

        public Response SendMessage(Message message)
        {
            UserData receiverUser = null;

            var dbReceiver = (from usr in context.Users
                              where usr.Username.Trim() == message.Receiver.Username.Trim()
                              select usr).FirstOrDefault();

            var sender = (from usr in context.Users
                               where usr.Username.Trim() == message.Sender.Username.Trim()
                               select usr).FirstOrDefault();

            var receiver = (from usr in context.Users
                                 where usr.Username.Trim() == message.Receiver.Username.Trim()
                                 select usr).FirstOrDefault();

            message.Sender = sender;
            message.Receiver = receiver;

            context.Messages.Add(message);

            context.SaveChanges();

            if (loggedUsers.TryGetValue(dbReceiver.Id, out receiverUser))
            {
                receiverUser.Callback.NotifyNewMessage(message);
            }
            
            return Response.Succes;
        }

        public User[] GetFriends(User user)
        {
            List<User> result = new List<User>();

            var dbUser = (from usr in context.Users
                          where usr.Username.Trim() == user.Username.Trim()
                          select usr).FirstOrDefault();

            var partB = from link in context.FriendshipLinks
                        where link.PartA.Id == dbUser.Id
                        select link.PartB;

            var partA = from link in context.FriendshipLinks
                        where link.PartB.Id == dbUser.Id
                        select link.PartA;

            if (partA != null)
            {
                foreach (User friend in partA)
                {
                    var msgs = from msg in context.Messages
                               where msg.Sender.Id == friend.Id && msg.Receiver.Id == dbUser.Id && !msg.Seen
                               select msg;

                    int count = msgs.Count();

                    friend.HasUnread = count > 0;

                    friend.Status = loggedUsers.ContainsKey(friend.Id) ? Status.Online : Status.Offline;

                    result.Add(friend);
                }
            }

            if (partB != null)
            {
                foreach (User friend in partB)
                {
                    var msgs = from msg in context.Messages
                               where msg.Sender.Id == friend.Id && msg.Receiver.Id == dbUser.Id && !msg.Seen
                               select msg;

                    int count = msgs.Count();

                    friend.HasUnread = count > 0;
                    
                    friend.Status = loggedUsers.ContainsKey(friend.Id) ? Status.Online : Status.Offline;

                    result.Add(friend);
                }

            }
            
            return result.ToArray();
        }

        public Message[] GetMessages(User owner, User partner)
        {
            var dbUserOwner = (from usr in context.Users
                               where usr.Username.Trim() == owner.Username.Trim()
                               select usr).FirstOrDefault();

            var dbUserPartner = (from usr in context.Users
                                 where usr.Username.Trim() == partner.Username.Trim()
                                 select usr).FirstOrDefault();

            var messages = from msg in context.Messages
                           where msg.Sender.Id == dbUserOwner.Id && msg.Receiver.Id == dbUserPartner.Id ||
                                 msg.Sender.Id == dbUserPartner.Id && msg.Receiver.Id == dbUserOwner.Id
                           select msg;

            return messages.ToArray();
        }

        public Response AddFriend(User fromUser, User toUser)
        {
            var uFrom =  (from usr in context.Users
                          where usr.Username.Trim() == fromUser.Username.Trim()
                               select usr).FirstOrDefault();

            var uTo =  (from usr in context.Users
                        where usr.Username.Trim() == toUser.Username.Trim()
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
                         where usr.Username.Trim() == fromUser.Username.Trim()
                         select usr).FirstOrDefault();

            var uTo = (from usr in context.Users
                       where usr.Username.Trim() == toUser.Username.Trim()
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
            
            return 0;
        }
    }
}
