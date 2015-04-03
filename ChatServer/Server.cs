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

            var uri = new Uri("net.tcp://192.168.1.3:9191/chat/");

            ServiceHost service = new ServiceHost(typeof(ChatService), uri);

            service.Open();

            Console.WriteLine("service started");

            Console.ReadKey();
        }
    }
}
