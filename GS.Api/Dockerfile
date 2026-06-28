FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GS.Api/GS.Api.csproj", "GS.Api/"]
COPY ["GS.Aplicacion/GS.Aplicacion.csproj", "GS.Aplicacion/"]
COPY ["GS.Dominio/GS.Dominio.csproj", "GS.Dominio/"]
COPY ["GS.Infraestructura/GS.Infraestructura.csproj", "GS.Infraestructura/"]
COPY ["GS.Logging/GS.Logging.csproj", "GS.Logging/"]
RUN dotnet restore "GS.Api/GS.Api.csproj"
COPY . .
WORKDIR "/src/GS.Api"
RUN dotnet build "GS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/GS.Api/Assets ./Assets 
ENTRYPOINT ["dotnet", "GS.Api.dll"]