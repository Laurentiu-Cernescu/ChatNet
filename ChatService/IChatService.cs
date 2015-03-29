using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract(CallbackContract = typeof(ICallback))]
    interface IChatService
    {
        Response Login(User user);
        Response Logout(User user);

        Response SendMessage(Message message);

        User[] GetFriends(User user);

        Message[] GetMessages(User owner, User partner);

        Response AddFriend(User from, User to);

        Response RemoveFriend(User from, User to);
    }
}
