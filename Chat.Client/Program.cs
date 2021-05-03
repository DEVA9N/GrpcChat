using System;
using System.Threading.Tasks;
using Chat.Contracts;
using Grpc.Core;
using Grpc.Net.Client;

namespace Chat.Client
{
    class Program
    {
        private const int DefaultClientPort = 50052;
        private const int DefaultServerPort = 50051;

        /// <summary>
        /// The arguments are optional for the first instance. The second instance requires the argument "50051 50052"
        /// in order to be able to connect to the first instance.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var (clientPort, serverPort) = GetPorts(args);
            var client = CreateClient(clientPort);
            var server = CreateServer(serverPort);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Chat initialized (Rx:{serverPort} Tx:{clientPort})");

            var messenger = new ConsoleMessenger(client, server);
            await messenger.StartAsync();
        }

        private static (int clientPort, int serverPort) GetPorts(string[] args)
        {
            return (
                args.Length > 0 ? Convert.ToInt32(args[0]) : DefaultClientPort,
                args.Length > 1 ? Convert.ToInt32(args[1]) : DefaultServerPort
            );
        }

        private static ChatClient CreateClient(int port)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:" + port);
            var client = new ChatClient(channel);

            return client;
        }

        private static ChatServer CreateServer(int port)
        {
            var chatServer = new ChatServer();
            var server = new Server
            {
                Services = { Chat.Contracts.Chat.BindService(chatServer) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();

            return chatServer;
        }
    }
}