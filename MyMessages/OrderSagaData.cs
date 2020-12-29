using System;
using NServiceBus;

namespace MyMessages
{
    public class OrderSagaData : IContainSagaData
    {
        public string OrderId { get; set; }
        public long Amount { get; set; }
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
    }

    public class FinishSagaMessage : IMessage
    {
        public string OrderId { get; set; }
        public long Amount { get; set; }
        public string Authority { get; set; }
    }
}