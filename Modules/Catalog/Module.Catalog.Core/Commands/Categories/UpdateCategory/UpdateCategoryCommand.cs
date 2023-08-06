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

namespace Module.Catalog.Core.Commands.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(CategoryDto CategoryDto) : IRequest<CategoryDto>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.CategoryDto.Id }, cancellationToken);
            if (category == null)
                throw new Exception("Not Found");

            category.Name = request.CategoryDto.Name;
            category.Description = request.CategoryDto.Description;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
            return request.CategoryDto;
        }
    }

}
