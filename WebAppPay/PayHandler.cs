using System;
using System.Threading.Tasks;
using MyMessages;
using NServiceBus;

namespace WebAppPay
{
    //public class PayHandler : IHandleMessages<MyMessage>
    //{
    //    public async Task Handle(MyMessage message, IMessageHandlerContext context)
    //    {

    //        var result = new ResultMessage
    //        {
    //            Authority = Guid.NewGuid().GetHashCode().ToString(),
    //            Status = new Random().Next(1,10)
    //        };
    //        await context.Reply(result);

    //    }
    //}


    public class OrderSaga : //Saga<OrderSagaData>,
        IHandleMessages<ResultMessage>
    {
        //protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        //{
        //    //mapper.ConfigureMapping<MyMessage>(message => message.OrderId)
        //    //    .ToSaga(sagaData => sagaData.OrderId);
        //    mapper.ConfigureMapping<ResultMessage>(message => message.OrderId)
        //        .ToSaga(sagaData => sagaData.OrderId);
        //}
        
        public async Task Handle(ResultMessage message, IMessageHandlerContext context)
        {
            //MarkAsComplete();
            var result = new FinishSagaMessage
            {
                OrderId = message.OrderId,
                Amount = new Random().Next(1000, 500000),
                Authority = message.Authority
            };
            var option = new SendOptions();
            option.SetDestination("SamplesFinance");
            await context.Send(result, option);
            //await Task.CompletedTask;
        }
    }
}
