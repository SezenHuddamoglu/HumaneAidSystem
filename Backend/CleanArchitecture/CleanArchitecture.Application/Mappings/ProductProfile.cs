using AutoMapper;
using CleanArchitecture.Core.DTOs.Product;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Infrastructure.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
