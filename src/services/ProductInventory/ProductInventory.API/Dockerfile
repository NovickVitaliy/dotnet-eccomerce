FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/ProductInventory/ProductInventory.API/ProductInventory.API.csproj", "src/services/ProductInventory/ProductInventory.API/"]
COPY ["src/services/ProductInventory/ProductInventory.Business/ProductInventory.Business.csproj", "src/services/ProductInventory/ProductInventory.Business/"]
COPY ["src/services/ProductInventory/ProductInventory.DataAccess/ProductInventory.DataAccess.csproj", "src/services/ProductInventory/ProductInventory.DataAccess/"]
COPY ["src/services/ProductInventory/ProductInventory.Domain/ProductInventory.Domain.csproj", "src/services/ProductInventory/ProductInventory.Domain/"]
RUN dotnet restore "src/services/ProductInventory/ProductInventory.API/ProductInventory.API.csproj"
COPY . .
WORKDIR "/src/src/services/ProductInventory/ProductInventory.API"
RUN dotnet build "ProductInventory.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProductInventory.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductInventory.API.dll"]
