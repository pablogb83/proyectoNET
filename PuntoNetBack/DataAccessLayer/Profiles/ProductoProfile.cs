using AutoMapper;
using DataAccessLayer.Dtos.Productos;
using DataAccessLayer.Helpers;

namespace DataAccessLayer.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            //Source -> Target
            CreateMap<Plan, ProductoReadDto>();
        }
    }
}
