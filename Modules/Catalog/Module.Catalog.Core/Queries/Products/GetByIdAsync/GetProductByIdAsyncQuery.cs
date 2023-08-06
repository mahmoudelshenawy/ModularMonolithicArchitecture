using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;

namespace Module.Catalog.Core.Queries.Products.GetByIdAsync
{
    public record GetProductByIdAsyncQuery(int Id) : IRequest<ProductDto>;


    internal class GetProductByIdAsyncQueryHandler : IRequestHandler<GetProductByIdAsyncQuery, ProductDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdAsyncQueryHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Products.FindAsync(new object[] { request.Id });
            return _mapper.Map<ProductDto>(model);
        }
    }
}
