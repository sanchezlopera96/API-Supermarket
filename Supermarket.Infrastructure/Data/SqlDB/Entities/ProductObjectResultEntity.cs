namespace Supermarket.Infrastructure.Data.SqlDB.Entities
{
    public class ProductObjectResultEntity
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int MarcaProducto { get; set; }
        public string Marca { get; set; }
        public int CategoriaProducto { get; set; }
        public string Categoria { get; set; }
        public int UnidadMedidaProducto { get; set; }
        public string UnidadMedida { get; set; }
        public decimal PrecioProducto { get; set; }
        public int StockProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public bool EstadoProducto { get; set; }
    }
}
