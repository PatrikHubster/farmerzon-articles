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
            CreateMap<DAO.Article, DTO.ArticleOutput>();
            CreateMap<DTO.ArticleInput, DAO.Article>();

            // Person
            CreateMap<DAO.Person, DTO.PersonOutput>();

            // Unit
            CreateMap<DAO.Unit, DTO.UnitOutput>();
            CreateMap<DTO.UnitInput, DAO.Unit>();
        }
    }
}
