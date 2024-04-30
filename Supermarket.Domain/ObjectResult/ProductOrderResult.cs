namespace Supermarket.Domain.ObjectResult
{
    public class ProductOrderResult
    {
        public string OrderProductCode { get; set; }
        public string OrderProductName { get; set; }
        public int OrderProductQuantity { get; set; }
        public decimal OrderProductPrice { get; set; }
        public decimal OrderProductFullPrice { get; set; }
    }
}
