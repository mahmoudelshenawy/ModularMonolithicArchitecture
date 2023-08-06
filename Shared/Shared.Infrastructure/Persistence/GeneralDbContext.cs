using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Interfaces;
using Shared.Core.Outbox;
using Shared.Infrastructure.Interceptors;
using Shared.Infrastructure.Persistence;

namespace Shared.Infrastructure.Persistence
{
    public class GeneralDbContext : ModuleDbContext, IGeneralDbContext
    {
        public GeneralDbContext(DbContextOptions<GeneralDbContext> options, ICurrentUserService currentUserService,
            IMediator mediator, BackgroundDomainEventSaveChangesInterceptor backgroundDomainEventSaveChangesInterceptor)
            : base(options, currentUserService, mediator, backgroundDomainEventSaveChangesInterceptor)
        {
        }
        protected override string Schema => "General";
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<OutboxMessageConsumer> outboxMessageConsumers { get; set; }

    }
}
