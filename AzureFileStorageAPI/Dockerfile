#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["AzureFileStorageAPI/AzureFileStorageAPI.csproj", "AzureFileStorageAPI/"]
RUN dotnet restore "AzureFileStorageAPI/AzureFileStorageAPI.csproj"
COPY . .
WORKDIR "/src/AzureFileStorageAPI"
RUN dotnet build "AzureFileStorageAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzureFileStorageAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureFileStorageAPI.dll"]