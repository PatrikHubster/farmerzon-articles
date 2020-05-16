using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Implementations;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using FarmerzonGraphModel.Outputs;
using GraphQL.Types;

namespace FarmerzonDataAccess
{
    public class Query : ObjectGraphType
    {
        private FarmerzonContext Context { get; set; }
        private IAddressRepository AddressRepository { get; set; }
        private IArticleRepository ArticleRepository { get; set; }
        private ICityRepository CityRepository { get; set; }
        private ICountryRepository CountryRepository { get; set; }
        private IUnitRepository UnitRepository { get; set; }
        private IPersonRepository PersonRepository { get; set; }
        private IStateRepository StateRepository { get; set; }

        public Query(FarmerzonContext context)
        {
            Context = context;
            AddressRepository = new AddressRepository();
            ArticleRepository = new ArticleRepository();
            CityRepository = new CityRepository();
            CountryRepository = new CountryRepository();
            UnitRepository = new UnitRepository();
            PersonRepository = new PersonRepository();
            StateRepository = new StateRepository();

            Name = "Query";
            Field<ListGraphType<AddressType>>(
                name: "addresses",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "addressId"},
                    new QueryArgument<StringGraphType> {Name = "doorNumber"},
                    new QueryArgument<StringGraphType> {Name = "street"}
                },
                resolve: GetAddresses);
            
            Field<ListGraphType<ArticleType>>(
                name: "articles",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "articleId"},
                    new QueryArgument<StringGraphType> {Name = "name"},
                    new QueryArgument<StringGraphType> {Name = "description"},
                    new QueryArgument<FloatGraphType> {Name = "price"},
                    new QueryArgument<IntGraphType> {Name = "amount"},
                    new QueryArgument<FloatGraphType> {Name = "size"},
                    new QueryArgument<DateTimeGraphType> {Name = "createdAt"},
                    new QueryArgument<DateTimeGraphType> {Name = "updatedAt"}
                },
                resolve: GetArticles);

            Field<ListGraphType<CityType>>(
                name: "cities",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "cityId"},
                    new QueryArgument<StringGraphType> {Name = "zipCode"},
                    new QueryArgument<StringGraphType> {Name = "name"}
                },
                resolve: GetCities);

            Field<ListGraphType<CountryType>>(
                name: "countries",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "countryId"},
                    new QueryArgument<StringGraphType> {Name = "name"},
                    new QueryArgument<StringGraphType> {Name = "code"}
                },
                resolve: GetCountries);

            Field<ListGraphType<PersonType>>(
                name: "people",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "personId"},
                    new QueryArgument<StringGraphType> {Name = "userName"},
                    new QueryArgument<StringGraphType> {Name = "normalizedUserName"}
                },
                resolve: GetPeople);

            Field<ListGraphType<UnitType>>(
                name: "units",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "unitId"},
                    new QueryArgument<StringGraphType> {Name = "name"}
                },
                resolve: GetUnits);

            Field<ListGraphType<StateType>>(
                name: "states",
                arguments: new QueryArguments
                {
                    new QueryArgument<IdGraphType> {Name = "stateId"},
                    new QueryArgument<StringGraphType> {Name = "name"}
                },
                resolve: GetStates);
        }

        private async Task<IList<Address>> GetAddresses(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("addressId");
            var doorNumber = context.GetArgument<string>("doorNumber");
            var street = context.GetArgument<string>("street");
            
            await using (await Context.Database.BeginTransactionAsync())
            {
                return await AddressRepository.GetEntities(id, doorNumber, street, Context);
            }
        }
        
        private async Task<IList<Article>> GetArticles(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("articleId");
            var name = context.GetArgument<string>("name");
            var description = context.GetArgument<string>("description");
            var price = context.GetArgument<double?>("price");
            var amount = context.GetArgument<int?>("amount");
            var size = context.GetArgument<double?>("size");
            var createdAt = context.GetArgument<DateTime?>("createdAt");
            var updatedAt = context.GetArgument<DateTime?>("updatedAt");
            
            await using (await Context.Database.BeginTransactionAsync())
            {
                return await ArticleRepository.GetEntities(id, name, description, price, amount, size,
                    createdAt, updatedAt, Context);
            }
            
        }

        private async Task<IList<City>> GetCities(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("cityId");
            var zipCode = context.GetArgument<string>("zipCode");
            var name = context.GetArgument<string>("name");

            await using (await Context.Database.BeginTransactionAsync())
            {
                return await CityRepository.FindEntities(id, zipCode, name, Context);
            }
        }

        private async Task<IList<Country>> GetCountries(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("countryId");
            var name = context.GetArgument<string>("name");
            var code = context.GetArgument<string>("code");
            
            await using (await Context.Database.BeginTransactionAsync())
            {
                return await CountryRepository.GetEntities(id, name, code, Context);
            }
        }

        private async Task<IList<Person>> GetPeople(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("personId");
            var userName = context.GetArgument<string>("username");
            var normalizedUserName = context.GetArgument<string>("normalizedUsername");
            
            await using (await Context.Database.BeginTransactionAsync())
            {
                return await PersonRepository.GetEntities(id, userName, normalizedUserName, Context);
            }
        }

        private async Task<IList<Unit>> GetUnits(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("unitId");
            var name = context.GetArgument<string>("name");
            
            await using (await Context.Database.BeginTransactionAsync())
            {
                return await UnitRepository.GetEntities(id, name, Context);
            }
        }
        
        private async Task<IList<State>> GetStates(ResolveFieldContext<object> context)
        {
            var id = context.GetArgument<int?>("stateId");
            var name = context.GetArgument<string>("name");
            
            await using (await Context.Database.BeginTransactionAsync())
            {
                return await StateRepository.GetEntities(id, name, Context);
            }
        }
    }
}