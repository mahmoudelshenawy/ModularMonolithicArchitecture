namespace Module.Catalog.Core.Dtos
{
    public class ProductStockDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Increase { get; set; }
    }
}
