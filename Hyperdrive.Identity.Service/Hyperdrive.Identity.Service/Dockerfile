FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

USER $APP_UID

ENV ASPNETCORE_URLS=http://+:8070;https://+:8071

EXPOSE 8070
EXPOSE 8071

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY Hyperdrive.Identity.Service/Hyperdrive.Identity.Service.csproj Hyperdrive.Identity.Service/
COPY Hyperdrive.Identity.Application/Hyperdrive.Identity.Application.csproj Hyperdrive.Identity.Application/
COPY Hyperdrive.Identity.Domain/Hyperdrive.Identity.Domain.csproj Hyperdrive.Identity.Domain/
COPY Hyperdrive.Identity.Infrastructure/Hyperdrive.Identity.Infrastructure.csproj Hyperdrive.Identity.Infrastructure/

COPY Hyperdrive.Identity.Service/nuget.config nuget.config

RUN dotnet restore "Hyperdrive.Identity.Service/Hyperdrive.Identity.Service.csproj" --configfile "nuget.config" --locked-mode

COPY . .

WORKDIR /src/Hyperdrive.Identity.Service
RUN dotnet build "Hyperdrive.Identity.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hyperdrive.Identity.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hyperdrive.Identity.Service.dll"]
