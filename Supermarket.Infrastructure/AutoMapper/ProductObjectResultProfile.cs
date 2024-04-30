using AutoMapper;
using Supermarket.Domain.ObjectResult;
using Supermarket.Infrastructure.Data.SqlDB.Entities;

namespace Supermarket.Infrastructure.AutoMapper
{
    public class ProductObjectResultProfile : Profile
    {
        public ProductObjectResultProfile()
        {
            CreateMap<ProductObjectResult, ProductObjectResultEntity>()
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(dest => dest.CodigoProducto, opt => opt.MapFrom(s => s.ProductCode))
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(s => s.ProductName))
                .ForMember(dest => dest.MarcaProducto, opt => opt.MapFrom(s => s.ProductBrand))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(s => s.Brand))
                .ForMember(dest => dest.CategoriaProducto, opt => opt.MapFrom(s => s.ProductCategory))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(s => s.Category))
                .ForMember(dest => dest.UnidadMedidaProducto, opt => opt.MapFrom(s => s.ProductUnitMeasure))
                .ForMember(dest => dest.UnidadMedida, opt => opt.MapFrom(s => s.UnitMeasure))
                .ForMember(dest => dest.PrecioProducto, opt => opt.MapFrom(s => s.ProductPrice))
                .ForMember(dest => dest.StockProducto, opt => opt.MapFrom(s => s.ProductStock))
                .ForMember(dest => dest.DescripcionProducto, opt => opt.MapFrom(s => s.ProductDescription))
                .ForMember(dest => dest.EstadoProducto, opt => opt.MapFrom(s => s.ProductStatus))
                .ReverseMap();
        }
    }
}