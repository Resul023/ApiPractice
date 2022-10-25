using AutoMapper;
using StoreApi.Apps.AdminApp.DTOs.CategoryDtos;
using StoreApi.Apps.AdminApp.DTOs.ProductDtos;
using StoreApi.DATA.Entities;
using WebApplication1.Entities;

namespace StoreApi.Apps.AdminApp.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            //Category side
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryInProductGetDto>();
                //.ForMember(destination => destination.ProductCount, map => map.MapFrom(source => source.Products.Count));

            //Product side
            CreateMap<Product, ProductGetDto>();
            


        }
    }
}
