using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Common.Events;
using Shared.Core.Interfaces;

namespace Shared.Infrastructure.Idempotence
{
    public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly INotificationHandler<TDomainEvent> _decorated;
        private readonly IGeneralDbContext _context;

        public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, IGeneralDbContext context)
        {
            _decorated = decorated;
            _context = context;
        }

        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            string consumer = _decorated.GetType().Name;
            if(await _context.outboxMessageConsumers.AnyAsync(c => 
            (c.Id == notification.Id || c.GuidId == notification.GuidId) && 
            c.Name == consumer
            ))
            {
                return;
            }
            await _decorated.Handle(notification, cancellationToken);
            _context.outboxMessageConsumers.Add(new Core.Outbox.OutboxMessageConsumer
            {
                Id = notification.Id ?? 0,
                GuidId = notification.GuidId,
                Name = consumer
            });

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
