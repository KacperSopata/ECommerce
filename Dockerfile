# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiujemy solution i projekty (ścieżki relatywne do Dockerfile w folderze ECommerce)
COPY ECommerce.sln ./
COPY ECommerce/ECommerce.csproj ./ECommerce/
COPY ECommerce.Application/ECommerce.Application.csproj ./ECommerce.Application/
COPY ECommerce.Domain/ECommerce.Domain.csproj ./ECommerce.Domain/
COPY ECommerce.Infrastructure/ECommerce.Infrastructure.csproj ./ECommerce.Infrastructure/

# Kopiujemy resztę plików
COPY ECommerce/. ./ECommerce/
COPY ECommerce.Application/. ./ECommerce.Application/
COPY ECommerce.Domain/. ./ECommerce.Domain/
COPY ECommerce.Infrastructure/. ./ECommerce.Infrastructure/

# Przywracamy zależności i budujemy
RUN dotnet restore ECommerce.sln
RUN dotnet publish ECommerce.sln -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "ECommerce.dll"]
