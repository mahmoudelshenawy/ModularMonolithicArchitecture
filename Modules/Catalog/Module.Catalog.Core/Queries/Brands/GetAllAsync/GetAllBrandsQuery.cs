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

namespace Module.Catalog.Core.Queries.Brands.GetAllAsync
{
    public record GetAllBrandsQuery : IRequest<List<BrandDto>>;

    internal class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, List<BrandDto>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetAllBrandsQueryHandler(ICatalogDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<List<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _context.Brands.ToListAsync(cancellationToken);
            return _mapper.Map<List<BrandDto>>(brands);
        }
    }


}
