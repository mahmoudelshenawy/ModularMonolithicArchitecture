using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Commands.Categories.CreateCategory
{
    public record CreateCategoryCommand(CategoryDto CategoryDto) : IRequest<CategoryDto>;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.CategoryDto);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
