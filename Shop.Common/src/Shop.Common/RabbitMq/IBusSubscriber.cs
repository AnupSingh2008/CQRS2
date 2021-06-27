using System;
using Shop.Common.Messages;
using Shop.Common.Types;

namespace Shop.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, ShopException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, ShopException, IRejectedEvent> onError = null) 
            where TEvent : IEvent;
    }
}
