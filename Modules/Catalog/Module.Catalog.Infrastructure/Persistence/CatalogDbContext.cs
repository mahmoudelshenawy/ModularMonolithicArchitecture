using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Entities;
using Shared.Core.Interfaces;
using Shared.Infrastructure.Common;
using Shared.Infrastructure.Interceptors;
using Shared.Infrastructure.Persistence;

namespace Module.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : ModuleDbContext, ICatalogDbContext
    {
       // private readonly IMediator _mediator;
        protected override string Schema => "Catalog";
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediator mediator
            , ICurrentUserService currentUserService,
            BackgroundDomainEventSaveChangesInterceptor backgroundDomainEventSaveChangesInterceptor) 
            : base(options, currentUserService, mediator, backgroundDomainEventSaveChangesInterceptor)
        {
           // _mediator = mediator;
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //await _mediator.DispatchDomainEvents(this);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
