using GraphQL.Types;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticles.GraphInputType
{
    public class UnitInputType : InputObjectGraphType<DTO.Unit>
    {
        public UnitInputType()
        {
            Name = "Unit";
            Field<NonNullGraphType<StringGraphType>>(name: "name");
        }
    }
}