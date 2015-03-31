using System;
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

        public ClientModel()
        {
            
        }

        public void Connect()
        {
            InstanceContext context = new InstanceContext(this);

            service = new ChatServiceClient(context, "NetTcpBinding_IChatService");
        }

        public void PingServer()
        {
            int ping = service.PingService();

            Console.WriteLine(ping);
        }

        public void Dispose()
        {
            service.Close();
        }

        void IChatServiceCallback.NotifyNewMessage(Message message)
        {
            
        }

        void IChatServiceCallback.NotifyStatusChange(User user, Status newStatus)
        {
            
        }
    }
}
