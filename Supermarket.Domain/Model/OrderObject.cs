namespace Supermarket.Domain.Model
{
    public class OrderObject
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderIdentificationClient { get; set; }
        public string OrderNameClient { get; set; }
        public string OrderCorreoClient { get; set; }
        public ProductOrder[] ProductsOrder { get; set; }
        public int OrderPaymentMehod { get; set; }
        public string OrderAddress { get; set; }
        public string OrderConveyor { get; set; }
        public string OrderDescription { get; set; }
        public int OrderStatus { get; set; }
    }
}