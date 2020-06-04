FROM mcr.microsoft.com/dotnet/core/sdk:latest AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:5001

FROM base AS builder
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY ./FarmerzonArticles/*.csproj FarmerzonArticles/
COPY ./FarmerzonArticlesDataAccess/*.csproj FarmerzonArticlesDataAccess/
COPY ./FarmerzonArticlesDataAccessModel/*.csproj FarmerzonArticlesDataAccessModel/
COPY ./FarmerzonArticlesManager/*.csproj FarmerzonArticlesManager/
COPY ./FarmerzonArticlesDataTransferModel/*.csproj FarmerzonArticlesDataTransferModel/
RUN dotnet restore --verbosity detailed
COPY . .
WORKDIR /src/FarmerzonArticles
RUN dotnet build -c $Configuration -o /app

FROM builder AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FarmerzonArticles.dll"]

EXPOSE 5001
