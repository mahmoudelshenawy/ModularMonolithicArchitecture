using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;

namespace Module.Catalog.Core.Commands.Products.Updateproduct
{
    public record UpdateProductAsyncCommand(ProductDto ProductDto) : IRequest<ProductDto>;

    internal class UpdateProductAsyncCommandHandler : IRequestHandler<UpdateProductAsyncCommand, ProductDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductAsyncCommandHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductAsyncCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.ProductDto.Id });

            //var product2= (product) _mapper.Map(request.ProductDto, product, typeof(ProductDto), typeof(product));
            product.Name = request.ProductDto.Name;
            product.CategoryId = request.ProductDto.CategoryId;
            product.BrandId = request.ProductDto.BrandId;
            product.SupplierId = request.ProductDto.SupplierId;
            product.SellingPrice = request.ProductDto.SellingPrice;
            product.PurchasePrice = request.ProductDto.PurchasePrice;
            product.QuantityPerUnit = request.ProductDto.QuantityPerUnit;
            product.AlertQuantity = request.ProductDto.AlertQuantity;
            product.EnableStock = request.ProductDto.EnableStock;
            product.UnitsOnOrder = request.ProductDto.UnitsOnOrder;
            product.UnitsInStock = request.ProductDto.UnitsInStock;
            product.Type = request.ProductDto.Type;

            _context.Products.Update(product);
            var response = await _context.SaveChangesAsync(cancellationToken);
            if (response > 0) request.ProductDto.Success = true;
            else request.ProductDto.Success = false;

            return _mapper.Map<ProductDto>(product);
        }
    }
}
