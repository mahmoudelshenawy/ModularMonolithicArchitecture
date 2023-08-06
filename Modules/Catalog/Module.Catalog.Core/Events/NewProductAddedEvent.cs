using Module.Catalog.Core.Entities;
using Shared.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Events
{
    public class NewProductAddedEvent : BaseEvent
    {
        public Product _product { get; }

        public NewProductAddedEvent(Product product)
        {
            _product = product;
        }
    }
}
