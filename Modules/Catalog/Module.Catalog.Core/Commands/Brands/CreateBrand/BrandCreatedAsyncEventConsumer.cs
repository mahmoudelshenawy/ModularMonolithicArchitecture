using MassTransit;
using Microsoft.Extensions.Logging;

namespace Module.Catalog.Core.Commands.Brands.CreateBrand
{
    public sealed class BrandCreatedAsyncEventConsumer : IConsumer<BrandCreatedAsyncEvent>
    {
        private readonly ILogger<BrandCreatedAsyncEventConsumer> _logger;

        public BrandCreatedAsyncEventConsumer(ILogger<BrandCreatedAsyncEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BrandCreatedAsyncEvent> context)
        {
            _logger.LogInformation("===============================================================================>");
            _logger.LogInformation(context.Message.ToString());
            return Task.CompletedTask;
        }
    }
}
