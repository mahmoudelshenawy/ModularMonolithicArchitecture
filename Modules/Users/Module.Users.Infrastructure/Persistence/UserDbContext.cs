using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module.Users.Core.Abstractions;
using Module.Users.Core.Entities;
using Shared.Infrastructure.Common;
using Shared.Infrastructure.Persistence;
using Shared.Models.Core;

namespace Module.Users.Infrastructure.Persistence
{
    public class UserDbContext  : IdentityDbContext<ApplicationUser> , IUserDbContext
    {
        protected string Schema => "Users";
        private readonly IMediator _mediator;
        public UserDbContext(DbContextOptions<UserDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.UpdatedAt = DateTime.Now;
                    entity.Entity.CreatedAt = DateTime.Now;
                }
                if (entity.State == EntityState.Modified || entity.HasChangedOwnedEntities())
                {
                    entity.Entity.UpdatedAt = DateTime.Now;
                }
            }
            await _mediator.DispatchDomainEvents(this);
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
