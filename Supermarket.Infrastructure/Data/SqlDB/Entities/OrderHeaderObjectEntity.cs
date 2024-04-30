namespace Supermarket.Infrastructure.Data.SqlDB.Entities
{
    public class OrderHeaderObjectEntity
    {
        public int? Id { get; set; }
        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string IdentificacionCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
        public int MetodoPagoPedido { get; set; }
        public decimal MontoTotalPedido { get; set; }
        public string DireccionPedido { get; set; }
        public string TransportistaPedido { get; set; }
        public string DescripcionPedido { get; set; }
        public int EstadoPedido { get; set; }
    }
}


