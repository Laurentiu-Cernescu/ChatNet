using System;
using System.ServiceModel;
using WcfChatService;

namespace ChatServer
{
    class Server
    {
        static void Main(string[] args)
        {
            ServiceHost service = new ServiceHost(typeof(ChatService), new Uri("net.tcp://localhost:9191/chat/"));

            service.Open();

            Console.WriteLine("service started");

            Console.ReadKey();
        }
    }
}
