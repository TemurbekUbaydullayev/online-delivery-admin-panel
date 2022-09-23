using AutoMapper;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Customers;
using OnlineFoodOrdering.Service.DTOs.Foods;
using OnlineFoodOrdering.Service.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Service.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, CustomerForCreationDto>().ReverseMap();

            CreateMap<FoodType, FoodTypeForCreationDto>().ReverseMap();
            CreateMap<Food, FoodViewModel>().ReverseMap();
            CreateMap<Food, FoodForCreationDto>().ReverseMap();
            CreateMap<IQueryable<Food>, List<FoodViewModel>>().ReverseMap();

            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, OrderForCreationDto>().ReverseMap();
            CreateMap<IQueryable<Order>, List<OrderViewModel>>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailForCreationDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
            CreateMap<IQueryable<OrderDetail>, List<OrderDetailViewModel>>().ReverseMap();
        }
    }
}
