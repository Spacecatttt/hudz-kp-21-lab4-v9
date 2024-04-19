FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FoodWorker/FoodWorker.csproj", "FoodWorker/"]
COPY ["../Broker/Broker.csproj", "Broker/"]
RUN dotnet restore "./FoodWorker/FoodWorker.csproj"
COPY . .
WORKDIR "/src/FoodWorker"
RUN dotnet build "./FoodWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./FoodWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodWorker.dll"]