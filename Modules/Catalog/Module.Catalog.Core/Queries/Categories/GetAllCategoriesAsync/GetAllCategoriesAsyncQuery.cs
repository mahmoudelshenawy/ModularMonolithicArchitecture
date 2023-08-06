using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Queries.Categories.GetAllCategoriesAsync
{
    public record GetAllCategoriesAsyncQuery : IRequest<List<CategoryDto>>;

    public class GetAllCategoriesAsyncQueryHandler : IRequestHandler<GetAllCategoriesAsyncQuery, List<CategoryDto>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesAsyncQueryHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesAsyncQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ToListAsync(cancellationToken);
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}
