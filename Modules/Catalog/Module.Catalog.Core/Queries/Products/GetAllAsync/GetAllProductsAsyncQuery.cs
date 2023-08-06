using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Shared.Infrastructure.Common;

namespace Module.Catalog.Core.Queries.Products.GetAllAsync
{
    public record GetAllProductsAsyncQuery(int PageNumber = 1 , int PageSize =10) : IRequest<PaginatedList<ProductDto>>;
    

    internal class GetAllProductsAsyncQueryHandler : IRequestHandler<GetAllProductsAsyncQuery, PaginatedList<ProductDto>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsAsyncQueryHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDto>> Handle(GetAllProductsAsyncQuery request, CancellationToken cancellationToken)
        {
            //Option A ==> use mapper and List<> separately
            //var entities = await _context.Products.ToListAsync(cancellationToken);
            //var dtos = _mapper.Map<List<ProductDto>>(entities);
            //return dtos;

            //Option B ==> Use ProjectTo With Customized Pagination
         return await  _context.Products.OrderByDescending(x => x.CreatedAt)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }


}
