using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;
using Module.Catalog.Core.Events;

namespace Module.Catalog.Core.Commands.Products.CreateSupplier
{
    public record CreateProductAsyncCommand(ProductDto ProductDto) : IRequest<ProductDto>;

    internal class CreateProductAsyncCommandHandler : IRequestHandler<CreateProductAsyncCommand, ProductDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductAsyncCommandHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductAsyncCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Product>(request.ProductDto);
            await _context.Products.AddAsync(model);
            //Add Event On Creating New Product
            model.AddDomainEvent(new NewProductAddedEvent(model));
            var response = await _context.SaveChangesAsync(cancellationToken);
            if (response > 0) request.ProductDto.Success = true;
            else request.ProductDto.Success = false;

            return _mapper.Map<ProductDto>(model);
        }
    }


}
