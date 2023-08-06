namespace Module.Catalog.Core.Commands.Brands.CreateBrand
{
    public record BrandCreatedAsyncEvent
    {
        public int Id { get; set; }
        public string Name { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;

        public override string ToString()
        {
            return $@"Id ==>{Id} - Name ==> {Name} - Message ==> {Message}";
        }
    }
}
