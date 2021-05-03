using Grpc.Core;
using Grpc.Net.Client;

namespace Chat.Contracts
{
    public class ChatClient : Chat.ChatClient
    {
        public ChatClient(GrpcChannel grpcChannel)
            : base(grpcChannel)
        {

        }

        public override AsyncUnaryCall<MessageReply> SendMessageAsync(MessageRequest request, CallOptions options)
        {
            return base.SendMessageAsync(request, options);
        }
    }
}