namespace Supermarket.Infrastructure.Data.SqlDB.Entities
{
    public class OrderDetailObjectEntity
    {
        public int? Id { get; set; }
        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string CodigoProductoPedido { get; set; }
        public string NombreProductoPedido { get; set; }
        public int CantidadProductoPedido { get; set; }
        public decimal PrecioUnitarioProductoPedido { get; set; }
        public decimal PrecioTotalProductoPedido { get; set; }
    }
}