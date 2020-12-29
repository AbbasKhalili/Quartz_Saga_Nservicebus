using NServiceBus;

namespace MyMessages
{
    public class ResultMessage : IMessage
    {
        public string OrderId { get; set; }
        public string Authority { get; set; }
        public long Status { get; set; }
    }
}