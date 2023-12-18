using AutoMapper;
using InventoryDomain;
using InventoryDomain.DataTransferObjects;

namespace AINC.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define mapping from DTO to entity
            CreateMap<ProductDTO, Product>().ReverseMap();

            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
