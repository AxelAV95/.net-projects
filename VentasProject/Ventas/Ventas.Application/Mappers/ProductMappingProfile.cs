using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Application.Dtos.Request;
using Ventas.Application.Dtos.Response;
using Ventas.Domain.Entities;
using Ventas.Infrastructure.Commons.Bases.Response;

namespace Ventas.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Producto, ProductRequestDto>().ReverseMap();
            CreateMap<Producto, BaseEntityResponse<ProductResponseDto>>().ReverseMap();
            CreateMap<ProductResponseDto, Producto>().ReverseMap();
            CreateMap<Producto, ProductSelectResponseDto>().ReverseMap();
            CreateMap<BaseEntityResponse<Producto>, BaseEntityResponse<ProductResponseDto>>().ReverseMap();


        }
    }
}
