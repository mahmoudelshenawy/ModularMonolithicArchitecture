using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;

namespace Module.Catalog.Core.Queries.Suppliers.GetAllAsync
{
    public record GetAllProductsAsyncQuery : IRequest<List<SupplierDto>>;

    internal class GetAllSuppliersAsyncQueryHandler : IRequestHandler<GetAllProductsAsyncQuery, List<SupplierDto>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSuppliersAsyncQueryHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SupplierDto>> Handle(GetAllProductsAsyncQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _context.Suppliers.ToListAsync(cancellationToken);
            var dtos = _mapper.Map<List<SupplierDto>>(suppliers);
            return dtos;
        }
    }


}
