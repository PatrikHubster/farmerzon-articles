using AutoMapper;

namespace FarmerzonArticlesManager.Implementation
{
    public class AbstractManager
    {
        protected IMapper Mapper { get; set; }

        public AbstractManager(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}