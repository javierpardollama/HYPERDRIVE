FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
USER $APP_UID

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Hyperdrive.Storage.Service/Hyperdrive.Storage.Service/Hyperdrive.Storage.Service.csproj Hyperdrive.Storage.Service/
COPY Hyperdrive.Storage.Service/Hyperdrive.Storage.Application/Hyperdrive.Storage.Application.csproj Hyperdrive.Storage.Application/
COPY Hyperdrive.Storage.Service/Hyperdrive.Storage.Domain/Hyperdrive.Storage.Domain.csproj Hyperdrive.Storage.Domain/
COPY Hyperdrive.Storage.Service/Hyperdrive.Storage.Infrastructure/Hyperdrive.Storage.Infrastructure.csproj Hyperdrive.Storage.Infrastructure/

COPY Hyperdrive.Storage.Service/nuget.config nuget.config

RUN dotnet restore "Hyperdrive.Storage.Service/Hyperdrive.Storage.Service.csproj" --configfile "nuget.config" --locked-mode

COPY . .

WORKDIR /src/Hyperdrive.Storage.Service/Hyperdrive.Storage.Service
RUN dotnet build "Hyperdrive.Storage.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hyperdrive.Storage.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hyperdrive.Storage.Service.dll"]
