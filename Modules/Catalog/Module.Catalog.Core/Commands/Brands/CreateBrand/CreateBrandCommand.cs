using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;
using Shared.Core.Common.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Commands.Brands.CreateBrand
{
    public record CreateBrandCommand(BrandDto BrandDto) : IRequest<BrandDto>;


    internal class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        public CreateBrandCommandHandler(ICatalogDbContext context, IMapper mapper, IEventBus eventBus)
        {
            _context = context;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<BrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Brand>(request.BrandDto);
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync(cancellationToken);
            await _eventBus.PublishAsync(
                new BrandCreatedAsyncEvent
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Message = @$"the branch is created at {brand.CreatedAt} by  {brand.CreatedBy}"
                });
            return _mapper.Map<BrandDto>(brand);
        }
    }
}
