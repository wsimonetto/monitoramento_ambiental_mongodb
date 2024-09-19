# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["monitoramento_ambiental_mongodb.csproj", "./"]
RUN dotnet restore "./monitoramento_ambiental_mongodb.csproj"
COPY . ./
WORKDIR "/src/."
RUN dotnet build "./monitoramento_ambiental_mongodb.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["monitoramento_ambiental_mongodb.csproj", "."]
RUN dotnet restore "./monitoramento_ambiental_mongodb.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./monitoramento_ambiental_mongodb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./monitoramento_ambiental_mongodb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "monitoramento_ambiental_mongodb.dll"]
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "monitoramento_ambiental_mongodb.dll"]
