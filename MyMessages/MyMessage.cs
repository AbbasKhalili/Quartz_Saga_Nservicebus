using NServiceBus;

namespace MyMessages
{
    public class MyMessage : IMessage
    {
        public string OrderId { get; set; }
        public long Amount { get; set; }
    }
}
