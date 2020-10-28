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
            CreateMap<DTO.ArticleOutput, DAO.Article>();

            // Person
            CreateMap<DAO.Person, DTO.PersonOutput>();
            CreateMap<DTO.PersonOutput, DAO.Person>();

            // Unit
            CreateMap<DAO.Unit, DTO.UnitOutput>();
            CreateMap<DTO.UnitOutput, DAO.Unit>();
        }
    }
}
