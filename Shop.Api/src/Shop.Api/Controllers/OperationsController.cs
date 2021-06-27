using Shop.Api.Services;
using Shop.Common.RabbitMq;
using Shop.Api.Messages.Commands;
using Shop.Api.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OpenTracing;

namespace Shop.Api.Controllers
{
    [AllowAnonymous]
    public class OperationsController : BaseController
    {
        private readonly IOperationsService _operationsService;

        public OperationsController(IBusPublisher busPublisher,  ITracer tracer,
            IOperationsService operationsService) : base(busPublisher, tracer)
        {
            _operationsService = operationsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Single(await _operationsService.GetAsync(id));
    }
}
