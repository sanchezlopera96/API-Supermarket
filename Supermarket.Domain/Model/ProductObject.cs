namespace Supermarket.Domain.Model
{
    public class ProductObject
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductBrand { get; set; }
        public int ProductCategory { get; set; }
        public int ProductUnitMeasure { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescription { get; set; }
        public bool ProductStatus { get; set; }
    }
}