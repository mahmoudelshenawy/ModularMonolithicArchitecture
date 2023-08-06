using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Events;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Catalog.Core.Commands.Products.UpdateStock
{
    public record UpdateProductStockCommand(ProductStockDto ProductStockDto) : IRequest<Result>;

    internal class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand, Result>
    {
        private readonly ICatalogDbContext _context;

        public UpdateProductStockCommandHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.ProductStockDto.ProductId });
            if (product == null)
                throw new EntityNotFound("Product");
            if (!product.EnableStock)
                return Result.Failure(new List<string>() { $"product {product.Name} is not available" });

            if (!request.ProductStockDto.Increase)
            {
                //decrease stock
                if (product.UnitsInStock < request.ProductStockDto.Quantity)
                {
                    //insufficient quantity available
                    return Result.Failure(new List<string> { "insufficient quantity available" });
                }

                product.UnitsInStock -= request.ProductStockDto.Quantity;

                //check for alert stock
                if (product.AlertQuantity <= product.UnitsInStock)
                {
                    //notifyAdmin 
                    product.AddDomainEvent(new AlertProductQuantityDecreasedEvent(product));
                }

            }
            else
            {
                product.UnitsInStock += request.ProductStockDto.Quantity;
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }


}
