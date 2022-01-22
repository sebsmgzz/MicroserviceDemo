#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroserviceDemo.Application/MicroserviceDemo.Application.csproj", "MicroserviceDemo.Application/"]
COPY ["MicroserviceDemo.Infrastructure/MicroserviceDemo.Infrastructure.csproj", "MicroserviceDemo.Infrastructure/"]
COPY ["MicroserviceDemo.Domain/MicroserviceDemo.Domain.csproj", "MicroserviceDemo.Domain/"]

RUN dotnet restore "MicroserviceDemo.Application/MicroserviceDemo.Application.csproj"
COPY . .

WORKDIR "/src/MicroserviceDemo.Application"
RUN dotnet build "MicroserviceDemo.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroserviceDemo.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceDemo.Application.dll"]
