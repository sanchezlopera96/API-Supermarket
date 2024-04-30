namespace Supermarket.Domain.ObjectResult
{
    public class ProductObjectResult
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductBrand { get; set; }
        public string Brand { get; set; }
        public int ProductCategory { get; set; }
        public string Category { get; set; }
        public int ProductUnitMeasure { get; set; }
        public string UnitMeasure { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescription { get; set; }
        public bool ProductStatus { get; set; }
    }
}