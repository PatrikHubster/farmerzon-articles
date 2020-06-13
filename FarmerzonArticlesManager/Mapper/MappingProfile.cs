using AutoMapper;

using DAO = FarmerzonArticlesDataAccessModel;
using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Article
            CreateMap<DAO.Article, DTO.Article>();
            CreateMap<DTO.Article, DAO.Article>();

            // Person
            CreateMap<DAO.Person, DTO.Person>();
            CreateMap<DTO.Person, DAO.Person>();

            // Unit
            CreateMap<DAO.Unit, DTO.Unit>();
            CreateMap<DTO.Unit, DAO.Unit>();
        }
    }
}
