FROM mcr.microsoft.com/dotnet/core/sdk:latest AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:5001

FROM base AS builder
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./
COPY ./Farmerzon/*.csproj Farmerzon/
COPY ./FarmerzonDataAccess/*.csproj FarmerzonDataAccess/
COPY ./FarmerzonDataAccessModel/*.csproj FarmerzonDataAccessModel/
COPY ./FarmerzonGraphModel/*.csproj FarmerzonGraphModel/
RUN dotnet restore --verbosity detailed
COPY . .
WORKDIR /src/Farmerzon
RUN dotnet build -c $Configuration -o /app

FROM builder AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Farmerzon.dll"]

EXPOSE 5001
