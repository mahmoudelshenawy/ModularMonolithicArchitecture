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

namespace Module.Catalog.Core.Queries.Categories.GetCategoryByIdAsync
{
    public record GetCategoryByIdAsyncQuery(int id) : IRequest<CategoryDto>;

    public class GetCategoryByIdAsyncQueryHandler : IRequestHandler<GetCategoryByIdAsyncQuery, CategoryDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdAsyncQueryHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.id);

            var dto = _mapper.Map<CategoryDto>(category);

            return dto;
        }
    }
}
