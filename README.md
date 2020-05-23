#  farmerzon-articles

The next step for farmerzon-articles is to get splitted into different parts. This means currently only the big Farmerzon service exists. The next
step is to split this up into farmerzon-articles, farmerzon-orders and farmerzon-addresses. The steps which are done before splitting up you
can find in the following documentation.

## Development documentation

The next step was to also switch this service to use the DI container. It is necessary to do that that you register the following three things for
`GraphQL`:

1. Queries
2. InputTypes
3. Types
4. Repositories
5. Linking components

For the implementation in this specific case the following things has to get registerd:

```csharp
services.AddScoped<IAddressRepository, AddressRepository>();
services.AddScoped<IArticleRepository, ArticleRepository>();
services.AddScoped<ICityRepository, CityRepository>();
services.AddScoped<ICountryRepository, CountryRepository>();
services.AddScoped<IPersonRepository, PersonRepository>();
services.AddScoped<IStateRepository, StateRepository>();
services.AddScoped<IUnitRepository, UnitRepository>();

services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
services.AddSingleton<IDocumentWriter, DocumentWriter>();
services.AddScoped<ISchema, RootSchema>();
services.AddScoped<IDependencyResolver>(provider => new FuncDependencyResolver(provider.GetRequiredService));

services.AddScoped<RootQuery>();
services.AddScoped<RootMutation>();
services.AddScoped<AddressQueryType>();

services.AddScoped<AddressType>();
services.AddScoped<ArticleForOrderType>();
services.AddScoped<ArticleType>();
services.AddScoped<CityType>();
services.AddScoped<CountryType>();
services.AddScoped<OrderType>();
services.AddScoped<PaymentMethodType>();
services.AddScoped<PersonType>();
services.AddScoped<StateType>();
services.AddScoped<UnitType>();

services.AddScoped<PersonInputType>();
services.AddScoped<UnitInputType>();

services.AddGraphQL(o => o.ExposeExceptions = true).AddGraphTypes(ServiceLifetime.Scoped);
```

To execute the last line of this codesnippet it is necessary to add the nuget package `GraphQL.Server.Core`. Another thing that has to be mentioned
is that as well the database context has to be added. This is done with the following statement:

```csharp
services.AddDbContextPool<FarmerzonContext>(
    option => option.UseNpgsql(
        Configuration.GetConnectionString("Farmerzon"),
        x => x.MigrationsAssembly(nameof(Farmerzon))));
```

The special thing about that is that we are not adding a normal DbContext we are adding a pool. The difference is that the `AddDbContext` statement
adds by default the description how to generate for every request a context. The `AddDbContextPool` statement instead creates a pool of `DbContext`
and helps you to hold database connections open in a pool.

How the configuration for this service was done you can find under the follwoing links:

1. [Missing registrations](https://stackoverflow.com/questions/57984645/implementing-graphql-graphql-for-net-in-asp-net-core-why-is-my-queryobjectg)
2. [Missing IDocumentWriter](https://stackoverflow.com/questions/53248792/unable-to-resolve-service-for-type-graphql-http-idocumentwriter)

The next thing that has to be done to configure the authentication as `GraphQL` expects it to be done. You can find this under the following link:

* [Configure Authentication](https://stackoverflow.com/questions/53537521/how-to-implement-authorization-using-graphql-net-at-resolver-function-level)