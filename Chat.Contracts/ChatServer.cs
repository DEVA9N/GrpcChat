using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace Chat.Contracts
{
    public class ChatServer : Chat.ChatBase
    {
        public event EventHandler<ChatMessage> ChatMessageReceived;

        public override Task<MessageReply> SendMessage(MessageRequest request, ServerCallContext context)
        {
            var message = new ChatMessage { Text = request.Text, SenderName = request.Sender };
            ChatMessageReceived?.Invoke(this, message);

            return Task.FromResult(new MessageReply { Received = true, Read = false });
        }
    }
}