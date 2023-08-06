using AutoMapper;
using MediatR;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;

namespace Module.Catalog.Core.Commands.Suppliers.CreateSupplier
{
    public record CreateProductAsyncCommand(SupplierDto SupplierDto) : IRequest<SupplierDto>;

    internal class CreateSupplierAsyncCommandHandler : IRequestHandler<CreateProductAsyncCommand, SupplierDto>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;

        public CreateSupplierAsyncCommandHandler(ICatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SupplierDto> Handle(CreateProductAsyncCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Supplier>(request.SupplierDto);
            await _context.Suppliers.AddAsync(model);
            var response = await _context.SaveChangesAsync(cancellationToken);
            if (response > 0) request.SupplierDto.Success = true;
            else request.SupplierDto.Success = false;

            return _mapper.Map<SupplierDto>(model);
        }
    }


}
