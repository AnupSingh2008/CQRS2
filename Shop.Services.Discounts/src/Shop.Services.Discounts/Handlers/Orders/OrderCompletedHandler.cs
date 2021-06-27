using System.Net.Http;
using System.Threading.Tasks;
using Shop.Common.Consul;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Discounts.Dto;
using Shop.Services.Discounts.Messages.Events;
using Shop.Services.Discounts.Services;
using Newtonsoft.Json;

namespace Shop.Services.Discounts.Handlers.Orders
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly IOrdersService _ordersService;
        private readonly ConsulHttpClient _consulHttpClient;

        public OrderCompletedHandler(IOrdersService ordersService,ConsulHttpClient consulHttpClient)
        {
            _ordersService = ordersService;
            _consulHttpClient = consulHttpClient;
        }
        
        public async Task HandleAsync(OrderCompleted @event, ICorrelationContext context)
        {
            // Hard-coded way
            // Level 0
            //            var response = await _httpClient.GetAsync($"http://localhost:5005/orders/{@event.Id}");
            //            var content = await response.Content.ReadAsStringAsync();
            //            var orderDto = JsonConvert.DeserializeObject<OrderDetailsDto>(content);

            // Level 1 - store microservice URL in appsettings.json e.g. for RestEase

            // Level 2 - use service discovery
            var orderDto = await _consulHttpClient
                .GetAsync<OrderDetailsDto>($"orders-service/orders/{@event.Id}");

            // Level 3 -  additional load balancer - Fabio
            // var response = await _httpClient.GetAsync($"http://localhost:9999/orders-service/orders/{@event.Id}");

            // Level 4
            // var orderDto = await _ordersService.GetAsync(@event.Id);

            await Task.CompletedTask;
        }
    }
}