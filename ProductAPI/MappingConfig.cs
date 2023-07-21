using AutoMapper;
using ProductAPI.Models;
using ProductAPI.Models.Dto;

namespace Services.ProductApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config=>
            {
                config.CreateMap<ProductDTO,Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
