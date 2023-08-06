using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Outbox
{
    public sealed class OutboxMessageConsumer
    {
        public int Id { get; set; }
        public Guid? GuidId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
