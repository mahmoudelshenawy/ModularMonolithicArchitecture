using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Queries.Brands.GetByIdAsync
{
    public record GetByIdAsyncQuery(int id) : IRequest<BrandDto>;

    public class GetByIdAsyncQueryHandler : IRequestHandler<GetByIdAsyncQuery, BrandDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdAsyncQueryHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BrandDto> Handle(GetByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == request.id);
            return _mapper.Map<BrandDto>(brand);
        }
    }
}
