FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VehicleLeasing.API/VehicleLeasing.API.csproj", "VehicleLeasing.API/"]
COPY ["VehicleLeasing.Domain/VehicleLeasing.Domain.csproj", "VehicleLeasing.Domain/"]
RUN dotnet restore "VehicleLeasing.API/VehicleLeasing.API.csproj"
COPY . .
WORKDIR "/src/VehicleLeasing.API"
RUN dotnet build "VehicleLeasing.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VehicleLeasing.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleLeasing.API.dll"]
