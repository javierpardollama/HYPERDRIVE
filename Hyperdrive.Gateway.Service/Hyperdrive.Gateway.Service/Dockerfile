FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
USER $APP_UID

ENV ASPNETCORE_URLS=http://+:8060
EXPOSE 8060

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Service.csproj Hyperdrive.Gateway.Service/
COPY Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Application/Hyperdrive.Gateway.Application.csproj Hyperdrive.Gateway.Application/
COPY Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Domain/Hyperdrive.Gateway.Domain.csproj Hyperdrive.Gateway.Domain/
COPY Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Infrastructure/Hyperdrive.Gateway.Infrastructure.csproj Hyperdrive.Gateway.Infrastructure/

COPY Hyperdrive.Gateway.Service/nuget.config nuget.config

RUN dotnet restore "Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Service.csproj" --configfile "nuget.config" --locked-mode

COPY . .

WORKDIR /src/Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Service
RUN dotnet build "Hyperdrive.Gateway.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hyperdrive.Gateway.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hyperdrive.Gateway.Service.dll"]
