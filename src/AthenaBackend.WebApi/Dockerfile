#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AthenaBackend.WebApi/AthenaBackend.WebApi.csproj", "AthenaBackend.WebApi/"]
RUN dotnet restore "AthenaBackend.WebApi/AthenaBackend.WebApi.csproj"
COPY . .
WORKDIR "/src/AthenaBackend.WebApi"
RUN dotnet build "AthenaBackend.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AthenaBackend.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AthenaBackend.WebApi.dll"]