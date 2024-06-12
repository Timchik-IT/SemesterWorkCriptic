FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Criptic.API/Criptic.API.csproj", "Criptic.API/"]
COPY ["Criptic.Application/Criptic.Application.csproj", "Criptic.Application/"]
COPY ["Criptic.Core/Criptic.Core.csproj", "Criptic.Core/"]
COPY ["Criptic.DataAccess/Criptic.DataAccess.csproj", "Criptic.DataAccess/"]
RUN dotnet restore "Criptic.API/Criptic.API.csproj"
COPY . .
WORKDIR "/src/Criptic.API"
RUN dotnet build "Criptic.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Criptic.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Criptic.API.dll"]
