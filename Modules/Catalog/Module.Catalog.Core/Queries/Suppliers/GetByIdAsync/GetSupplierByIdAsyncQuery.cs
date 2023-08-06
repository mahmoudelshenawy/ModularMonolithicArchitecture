using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;

namespace Module.Catalog.Core.Queries.Suppliers.GetByIdAsync
{
    public record GetProductByIdAsyncQuery(int Id) : IRequest<SupplierDto>;


    internal class GetSupplierByIdAsyncQueryHandler : IRequestHandler<GetProductByIdAsyncQuery, SupplierDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetSupplierByIdAsyncQueryHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SupplierDto> Handle(GetProductByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(new object[] { request.Id });
            return _mapper.Map<SupplierDto>(supplier);
        }
    }
}
