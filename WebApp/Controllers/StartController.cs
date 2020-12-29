using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMessages;
using NServiceBus;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StartController : ControllerBase
    {
        private readonly IMessageSession _messageSession;

        public StartController(IMessageSession messageSession)
        {
            this._messageSession = messageSession;
        }


        [HttpGet]
        public async Task<string> Get()
        {
            var message = new MyMessage()
            {
                OrderId = Guid.NewGuid().ToString(),
                Amount = new Random().Next(1000,500000)
            };
            //var option = new SendOptions();
            //option.SetDestination("SamplesPayment");

            await _messageSession.SendLocal(message).ConfigureAwait(false);
            return "Message sent to endpoint";
        }
    }


    public class OrderSaga : Saga<OrderSagaData>,
        IAmStartedByMessages<MyMessage>,
        IHandleMessages<FinishSagaMessage>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
        {
            mapper.ConfigureMapping<MyMessage>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<FinishSagaMessage>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }

        public async Task Handle(MyMessage message, IMessageHandlerContext context)
        {
            //MarkAsComplete();
            var result = new ResultMessage
            {
                Authority = Guid.NewGuid().GetHashCode().ToString(),
                Status = new Random().Next(1, 10),
                OrderId = message.OrderId
            };
            var option = new SendOptions();
            option.SetDestination("SamplesPayment");
            await context.Send(result, option);
        }

        public async Task Handle(FinishSagaMessage message, IMessageHandlerContext context)
        {
            await ReplyToOriginator(context, message);
            MarkAsComplete();
        }
    }
}
