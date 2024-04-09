FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WeatherWorker/WeatherWorker.csproj", "WeatherWorker/"]
COPY ["../Broker/Broker.csproj", "Broker/"]
RUN dotnet restore "./WeatherWorker/WeatherWorker.csproj"
COPY . .
WORKDIR "/src/WeatherWorker"
RUN dotnet build "./WeatherWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WeatherWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Broker/rabbitSetting.json", "Broker/rabbitSetting.json"]
ENTRYPOINT ["dotnet", "WeatherWorker.dll"]