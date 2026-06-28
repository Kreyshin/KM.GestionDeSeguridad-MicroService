# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["GS.Api/GS.Api.csproj", "GS.Api/"]
COPY ["GS.Aplicacion/GS.Aplicacion.csproj", "GS.Aplicacion/"]
COPY ["GS.Infraestructura/GS.Infraestructura.csproj", "GS.Infraestructura/"]
COPY ["GS.Logging/GS.Logging.csproj", "GS.Logging/"]
COPY ["GS.Dominio/GS.Dominio.csproj", "GS.Dominio/"]

# Restore dependencies
RUN dotnet restore "GS.Api/GS.Api.csproj"

# Copy entire source
COPY . .

# Build the application
RUN dotnet build "GS.Api/GS.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "GS.Api/GS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Create directories for logs
RUN mkdir -p /app/Logs /app/Assets

# Copy published application
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port
EXPOSE 80

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/health || exit 1

# Run the application
ENTRYPOINT ["dotnet", "GS.Api.dll"]
