using Supermarket.Domain.Model;

namespace Supermarket.Domain.ObjectResult
{
    public class OrderObjectResult
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderIdentificationClient { get; set; }
        public string OrderNameClient { get; set; }
        public string OrderCorreoClient { get; set; }
        public ProductOrderResult[] ProductsOrderResult { get; set; }
        public int OrderPaymentMehod { get; set; }
        public string OrderPaymentMehodName { get; set; }
        public decimal OrderFullPayment { get; set; }
        public string OrderAddress { get; set; }
        public string OrderConveyor { get; set; }
        public string OrderDescription { get; set; }
        public int OrderStatus { get; set; }
    }
}



