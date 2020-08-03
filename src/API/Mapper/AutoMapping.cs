using API.Models;
using AutoMapper;
using Domain.Entities;

namespace API.Mapper
{
    //The object-to-object mapping configuration 
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SalesOrderDetail, OrderDetail>().ReverseMap();
            CreateMap<SalesOrderHeaderSalesReason, OrderSalesReason>().ReverseMap();
            CreateMap<Domain.Entities.SalesReason, API.Models.SalesReason>().ReverseMap();
            CreateMap<SalesOrderHeader, Order>()
                    .ForMember(o => o.OrderDetails, o => o.MapFrom(src => src.SalesOrderDetails))
                    .ForMember(o => o.OrderSalesReasons, o => o.MapFrom(src => src.SalesOrderHeaderSalesReasons))
                    .ReverseMap();
        }
    }
}