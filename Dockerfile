#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StenfordAPI/StenfordAPI.csproj", "StenfordAPI/"]
COPY ["Stenford.Common/Stenford.Common.csproj", "Stenford.Common/"]
COPY ["Stenford.Data/Stenford.Data.csproj", "Stenford.Data/"]
COPY ["Stenford.Domain/Stenford.Domain.csproj", "Stenford.Domain/"]
COPY ["Stenford.Postgres/Stenford.Postgres.csproj", "Stenford.Postgres/"]
COPY ["Stenford.Service/Stenford.Service.csproj", "Stenford.Service/"]
RUN dotnet restore "./StenfordAPI/StenfordAPI.csproj"
COPY . .
WORKDIR "/src/StenfordAPI"
RUN dotnet build "./StenfordAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StenfordAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StenfordAPI.dll"]