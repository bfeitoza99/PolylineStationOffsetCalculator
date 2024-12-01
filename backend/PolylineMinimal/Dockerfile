FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["../PolylineMinimalApi/PolylineMinimal.Api.csproj", "PolylineMinimalApi/"]
COPY ["../PolylineMinimal.Application/PolylineMinimal.Application.csproj", "PolylineMinimal.Application/"]
COPY ["../PolylineMinimalDomain/PolylineMinimal.Domain.csproj", "PolylineMinimalDomain/"]
COPY ["../PolylineMinimal.Infra/PolylineMinimal.Infra.csproj", "PolylineMinimal.Infra/"]
RUN dotnet restore "./PolylineMinimalApi/PolylineMinimal.Api.csproj"

COPY . .
WORKDIR "/src/PolylineMinimalApi"
RUN dotnet build "./PolylineMinimal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PolylineMinimal.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PolylineMinimal.Api.dll"]