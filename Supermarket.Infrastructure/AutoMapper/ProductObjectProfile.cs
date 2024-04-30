using AutoMapper;
using Supermarket.Domain.Model;
using Supermarket.Infrastructure.Data.SqlDB.Entities;

namespace Supermarket.Infrastructure.AutoMapper
{
    public class ProductObjectProfile : Profile
    {
        public ProductObjectProfile()
        {
            CreateMap<ProductObject, ProductObjectEntity>()
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(dest => dest.CodigoProducto, opt => opt.MapFrom(s => s.ProductCode))
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(s => s.ProductName))
                .ForMember(dest => dest.MarcaProducto, opt => opt.MapFrom(s => s.ProductBrand))
                .ForMember(dest => dest.CategoriaProducto, opt => opt.MapFrom(s => s.ProductCategory))
                .ForMember(dest => dest.UnidadMedidaProducto, opt => opt.MapFrom(s => s.ProductUnitMeasure))
                .ForMember(dest => dest.PrecioProducto, opt => opt.MapFrom(s => s.ProductPrice))
                .ForMember(dest => dest.StockProducto, opt => opt.MapFrom(s => s.ProductStock))
                .ForMember(dest => dest.DescripcionProducto, opt => opt.MapFrom(s => s.ProductDescription))
                .ForMember(dest => dest.EstadoProducto, opt => opt.MapFrom(s => s.ProductStatus))
                .ReverseMap();
        }
    }
}