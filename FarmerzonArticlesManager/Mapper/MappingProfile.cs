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
            CreateMap<DAO.Article, DTO.ArticleResponse>();
            CreateMap<DTO.ArticleResponse, DAO.Article>();

            // Person
            CreateMap<DAO.Person, DTO.PersonResponse>();
            CreateMap<DTO.PersonResponse, DAO.Person>();

            // Unit
            CreateMap<DAO.Unit, DTO.UnitResponse>();
            CreateMap<DTO.UnitResponse, DAO.Unit>();
        }
    }
}
