using System;
using System.Threading.Tasks;
using Chat.Contracts;

namespace Chat.Client
{
    internal sealed class ConsoleMessenger
    {

        private readonly ChatClient _client;
        private readonly ChatServer _server;
        private readonly String _userName;

        public ConsoleMessenger(ChatClient client, ChatServer server)
        {
            _client = client;
            _server = server;
            _server.ChatMessageReceived += Server_ChatMessageReceived;

            _userName = ConsoleUser.GetRandomUserName();
        }
    
        public Task StartAsync()
        {
            return Task.Run(async () =>
            {
                string message;

                while ((message = Console.ReadLine())?.ToLower() != "quit")
                {
                    await _client.SendMessageAsync(new MessageRequest { Text = message, Sender = _userName });
                }
            });
        }

        private void Server_ChatMessageReceived(object sender, ChatMessage message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine($"{message.SenderName}: {message.Text}");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}