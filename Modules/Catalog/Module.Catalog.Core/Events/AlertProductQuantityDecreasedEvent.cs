using Module.Catalog.Core.Entities;
using Shared.Models.Core;

namespace Module.Catalog.Core.Events
{
    internal class AlertProductQuantityDecreasedEvent : BaseEvent
    {
        public Product _product { get; set; }

        public AlertProductQuantityDecreasedEvent(Product product)
        {
            _product = product;
        }
    }
}
