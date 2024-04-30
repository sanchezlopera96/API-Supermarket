using Supermarket.Domain.Model;

namespace Supermarket.Domain.ObjectResult
{
    public class OrderObjectResultEntity
    {
        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string IdentificacionCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string CodigoProductoPedido { get; set; }
        public string NombreProductoPedido { get; set; }
        public int CantidadProductoPedido { get; set; }
        public decimal PrecioUnitarioProductoPedido { get; set; }
        public decimal PrecioTotalProductoPedido { get; set; }
        public int MetodoPagoPedido { get; set; }
        public string NombreMetodoPagoPedido { get; set; }
        public decimal MontoTotalPedido { get; set; }
        public string DireccionPedido { get; set; }
        public string TransportistaPedido { get; set; }
        public string DescripcionPedido { get; set; }
        public int EstadoPedido { get; set; }
    }
}
