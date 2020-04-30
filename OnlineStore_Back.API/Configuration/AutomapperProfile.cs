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
            CreateMap<Category, CategoryOutputModel>();
            CreateMap<City, CityOutputModel>();

            CreateMap<Product, ProductOutputModel>()
                //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Category.Name));

            //CreateMap<Order_ProductInputModel, Order_Product>()
            //    .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<Order, OrderOutputModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString(@"dd.MM.yyyy")))
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
        }
    }
}
