using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;

namespace Module.Catalog.Core.Commands.Brands.UpdateBrand
{
    public record UpdateBrandCommand(BrandDto BrandDto) : IRequest<BrandDto>;


    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _context.Brands.FindAsync(new object[] { request.BrandDto.Id} , cancellationToken);
            if (brand == null)
                throw new Exception("Not Found");

            brand.Name = request.BrandDto.Name;
            brand.Description = request.BrandDto.Description;
             _context.Brands.Update(brand);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BrandDto>(brand);
        }
    }
}
