#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Regulus.API/Regulus.API.csproj", "Regulus.API/"]
COPY ["Regulus.Common/Regulus.Common.csproj", "Regulus.Common/"]
RUN dotnet restore "Regulus.API/Regulus.API.csproj"
COPY . .
WORKDIR "/src/Regulus.API"
RUN dotnet build "Regulus.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Regulus.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Regulus.API.dll"]