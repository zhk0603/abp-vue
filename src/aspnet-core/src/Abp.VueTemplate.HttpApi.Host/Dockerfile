#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Abp.VueTemplate.HttpApi.Host/Abp.VueTemplate.HttpApi.Host.csproj", "src/Abp.VueTemplate.HttpApi.Host/"]
COPY ["src/Abp.VueTemplate.HttpApi/Abp.VueTemplate.HttpApi.csproj", "src/Abp.VueTemplate.HttpApi/"]
COPY ["src/Abp.VueTemplate.Application.Contracts/Abp.VueTemplate.Application.Contracts.csproj", "src/Abp.VueTemplate.Application.Contracts/"]
COPY ["src/Abp.VueTemplate.Domain.Shared/Abp.VueTemplate.Domain.Shared.csproj", "src/Abp.VueTemplate.Domain.Shared/"]
COPY ["src/Abp.VueTemplate.Application/Abp.VueTemplate.Application.csproj", "src/Abp.VueTemplate.Application/"]
COPY ["src/Abp.VueTemplate.Domain/Abp.VueTemplate.Domain.csproj", "src/Abp.VueTemplate.Domain/"]
COPY ["src/Abp.VueTemplate.EntityFrameworkCore.DbMigrations/Abp.VueTemplate.EntityFrameworkCore.DbMigrations.csproj", "src/Abp.VueTemplate.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Abp.VueTemplate.EntityFrameworkCore/Abp.VueTemplate.EntityFrameworkCore.csproj", "src/Abp.VueTemplate.EntityFrameworkCore/"]
RUN dotnet restore "src/Abp.VueTemplate.HttpApi.Host/Abp.VueTemplate.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Abp.VueTemplate.HttpApi.Host"
RUN dotnet build "Abp.VueTemplate.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Abp.VueTemplate.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Abp.VueTemplate.HttpApi.Host.dll"]