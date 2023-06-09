#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BuildingConstructApi/BuildingConstructApi.csproj", "BuildingConstructApi/"]
COPY ["Constants/Constants.csproj", "Constants/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["BuildingConstructApplication/Application.csproj", "BuildingConstructApplication/"]
COPY ["ViewModels/ViewModels.csproj", "ViewModels/"]
RUN dotnet restore "BuildingConstructApi/BuildingConstructApi.csproj"
COPY . .
WORKDIR "/src/BuildingConstructApi"
RUN dotnet build "BuildingConstructApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuildingConstructApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildingConstructApi.dll"]