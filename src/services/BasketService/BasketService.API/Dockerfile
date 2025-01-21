FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/BasketService/BasketService.API/BasketService.API.csproj", "src/services/BasketService/BasketService.API/"]
COPY ["src/services/BasketService/BasketService.Application/BasketService.Application.csproj", "src/services/BasketService/BasketService.Application/"]
COPY ["src/common/Common/Common.csproj", "src/common/Common/"]
COPY ["src/services/BasketService/BasketService.Domain/BasketService.Domain.csproj", "src/services/BasketService/BasketService.Domain/"]
COPY ["src/services/BasketService/BasketService.Infrastructure/BasketService.Infrastructure.csproj", "src/services/BasketService/BasketService.Infrastructure/"]
RUN dotnet restore "src/services/BasketService/BasketService.API/BasketService.API.csproj"
COPY . .
WORKDIR "/src/src/services/BasketService/BasketService.API"
RUN dotnet build "BasketService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BasketService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasketService.API.dll"]
