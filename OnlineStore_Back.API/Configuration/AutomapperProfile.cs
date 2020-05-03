using AutoMapper;
using OnlineStoreBack.DB.Models;
using OnlineStoreBack.API.Models.InputModels;
using OnlineStoreBack.API.Models.OutputModels;

namespace OnlineStore_Back.API.Configuration
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<TotalCostByCountry, TotalCostOutputModel>();

            CreateMap<OrderByTimeSpan, OrderByTimeSpanOutputModel>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString(@"dd.MM.yyyy HH:mm:ss")))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Product.Model))
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<City, MostSoldProductInCityOutputModel>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Product, ProductOutputModel>()
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Order, OrderOutputModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString(@"dd.MM.yyyy HH:mm:ss")))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            CreateMap<Order_Product, Order_ProductOutputModel>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Product.Model))
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.Product.Category.Name));

            CreateMap<OrderInputModel, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.ProductList))
                .ForPath(dest => dest.City.Id, opt => opt.MapFrom(src => src.CityId));

            CreateMap<Order_ProductInputModel, Order_Product>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<ProductSearchInputModel, ProductSearch>();

            CreateMap<CategoryWithProducts, CategoriesWithCountOfProductsOutputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CityTotalWorth, CityTotalWorthOutputModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.City.Name));
        }
    }
}
