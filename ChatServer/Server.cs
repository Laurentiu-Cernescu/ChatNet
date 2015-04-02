using System;
using System.ServiceModel;
using WcfChatService;

namespace ChatServer
{
    class Server
    {
        static void Main(string[] args)
        {
            ChatService.TestEntityFramework();

            ServiceHost service = new ServiceHost(typeof(ChatService), new Uri("net.tcp://192.168.1.3:9191/chat/"));

            service.Open();

            Console.WriteLine("service started");

            Console.ReadKey();
        }
    }
}
