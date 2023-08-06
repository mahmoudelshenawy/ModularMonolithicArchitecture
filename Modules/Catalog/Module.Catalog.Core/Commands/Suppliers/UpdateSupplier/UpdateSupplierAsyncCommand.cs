using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;

namespace Module.Catalog.Core.Commands.Suppliers.UpdateSupplier
{
    public record UpdateProductAsyncCommand(SupplierDto SupplierDto) : IRequest<SupplierDto>;

    internal class UpdateSupplierAsyncCommandHandler : IRequestHandler<UpdateProductAsyncCommand, SupplierDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSupplierAsyncCommandHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SupplierDto> Handle(UpdateProductAsyncCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(new object[] { request.SupplierDto.Id });

            //var supplier2= (Supplier) _mapper.Map(request.SupplierDto, supplier, typeof(SupplierDto), typeof(Supplier));
            supplier.Address = request.SupplierDto.Address;
            supplier.CompanyName = request.SupplierDto.CompanyName;
            supplier.ContactName = request.SupplierDto.ContactName;
            supplier.City = request.SupplierDto.City;
            supplier.Region = request.SupplierDto.Region;
            supplier.Phone = request.SupplierDto.Phone;
            supplier.PostalCode = request.SupplierDto.PostalCode;
            supplier.Country = request.SupplierDto.Country;

            _context.Suppliers.Update(supplier);
            var response = await _context.SaveChangesAsync(cancellationToken);
            if (response > 0) request.SupplierDto.Success = true;
            else request.SupplierDto.Success = false;

            return _mapper.Map<SupplierDto>(supplier);
        }
    }
}
