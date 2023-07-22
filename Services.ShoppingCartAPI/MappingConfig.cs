﻿using AutoMapper;
using Services.ShoppingCartAPI.Models;
using Services.ShoppingCartAPI.Models.Dto;

namespace Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config=>
            {
                config.CreateMap<CartHeader,CartHeaderDTO>().ReverseMap();
                config.CreateMap<CartDetails,CartDetailsDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
