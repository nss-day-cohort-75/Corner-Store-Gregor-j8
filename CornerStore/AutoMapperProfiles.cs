using AutoMapper;
using CornerStore.Models;
using CornerStore.Models.DTO;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Cashier, CashierDTO>();
        CreateMap<CashierDTO, Cashier>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();
        CreateMap<OrderProduct, OrderProductDTO>();
        CreateMap<OrderProductDTO, OrderProduct>();
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, ProductDTO>();
    }
}