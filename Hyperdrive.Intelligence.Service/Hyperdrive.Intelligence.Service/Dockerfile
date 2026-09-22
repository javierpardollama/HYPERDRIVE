FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

USER $APP_UID

ENV ASPNETCORE_URLS=http://+:8090

EXPOSE 8090

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Service.csproj Hyperdrive.Intelligence.Service/
COPY Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Application/Hyperdrive.Intelligence.Application.csproj Hyperdrive.Intelligence.Application/
COPY Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Domain/Hyperdrive.Intelligence.Domain.csproj Hyperdrive.Intelligence.Domain/
COPY Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Infrastructure/Hyperdrive.Intelligence.Infrastructure.csproj Hyperdrive.Intelligence.Infrastructure/

COPY Hyperdrive.Intelligence.Service/nuget.config nuget.config

RUN dotnet restore "Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Service.csproj" --configfile "nuget.config" --locked-mode

COPY . .

WORKDIR /src/Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Service
RUN dotnet build "Hyperdrive.Intelligence.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hyperdrive.Intelligence.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hyperdrive.Intelligence.Service.dll"]
