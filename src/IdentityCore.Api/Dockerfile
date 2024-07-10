#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["IdentityCore.Api/IdentityCore.Api.csproj", "IdentityCore.Api/"]
COPY ["IdentityCore.Application/IdentityCore.Application.csproj", "IdentityCore.Application/"]
COPY ["IdentityCore.Domain/IdentityCore.Domain.csproj", "IdentityCore.Domain/"]
COPY ["IdentityCore.Infraestructure/IdentityCore.Infraestructure.csproj", "IdentityCore.Infraestructure/"]
RUN dotnet restore "./IdentityCore.Api/IdentityCore.Api.csproj"
COPY . .
WORKDIR "/src/IdentityCore.Api"
RUN dotnet build "IdentityCore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityCore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityCore.Api.dll"]