using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Core.Interfaces;
using Shared.Core.Outbox;
using Shared.Infrastructure.Common;
using Shared.Infrastructure.Interceptors;
using Shared.Models.Core;
using System.Text.Json;

namespace Shared.Infrastructure.Persistence
{
    public abstract class ModuleDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        private readonly BackgroundDomainEventSaveChangesInterceptor _backgroundDomainEventSaveChangesInterceptor;
        protected abstract string Schema { get; }
        protected ModuleDbContext(DbContextOptions options, ICurrentUserService currentUserService, IMediator mediator,
            BackgroundDomainEventSaveChangesInterceptor backgroundDomainEventSaveChangesInterceptor) : base(options)
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
            _backgroundDomainEventSaveChangesInterceptor = backgroundDomainEventSaveChangesInterceptor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_backgroundDomainEventSaveChangesInterceptor);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.UpdatedAt = DateTime.Now;
                    entity.Entity.CreatedAt = DateTime.Now;
                    entity.Entity.CreatedBy = _currentUserService.userId;
                }
                if (entity.State == EntityState.Modified || entity.HasChangedOwnedEntities())
                {
                    entity.Entity.CreatedAt = entity.Entity.CreatedAt;
                    entity.Entity.UpdatedAt = DateTime.Now;
                    entity.Entity.ModifiedBy = _currentUserService.userId;
                    entity.Entity.CreatedBy = entity.Entity.CreatedBy;
                }
            }
            await _mediator.DispatchDomainEvents(this);

            //BackgroundDomainEvents
           
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
