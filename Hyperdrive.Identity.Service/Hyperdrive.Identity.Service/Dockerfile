# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# Usa usuario no root
USER $APP_UID

# Opcional: fija los puertos HTTP/HTTPS expl�citamente (coincide con EXPOSE)
ENV ASPNETCORE_URLS=http://+:8080;https://+:8081

# Expone sólo los puertos necesarios.
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia únicamente archivos de proyecto para aprovechar el caché en el restore
COPY ["Hyperdrive.Identity.Service/Hyperdrive.Identity.Service.csproj", "Hyperdrive.Identity.Service/"]
COPY ["Hyperdrive.Identity.Application/Hyperdrive.Identity.Application.csproj", "Hyperdrive.Identity.Application/"]
COPY ["Hyperdrive.Identity.Domain/Hyperdrive.Identity.Domain.csproj", "Hyperdrive.Identity.Domain/"]
COPY ["Hyperdrive.Identity.Infrastructure/Hyperdrive.Identity.Infrastructure.csproj", "Hyperdrive.Identity.Infrastructure/"]
COPY ["Hyperdrive.Identity.Service/nuget.config", "./nuget.config"]

# Restore del proyecto principal (sin compilar) usando el runtime actual para cache estable
RUN dotnet restore "./Hyperdrive.Identity.Service/Hyperdrive.Identity.Service.csproj" --configfile "./nuget.config" --locked-mode

# Ahora copia el resto del código (esto invalida el caché sólo si cambian fuentes)
COPY . .

# Compila sin restaurar (ya hicimos restore), salida a una carpeta intermedia
WORKDIR "/src/Hyperdrive.Identity.Service"
RUN dotnet build "./Hyperdrive.Identity.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica sin compilar y sin restaurar, reduciendo tamaño final
RUN dotnet publish "Hyperdrive.Identity.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish 

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app

# Copia artefactos de publish
COPY --from=publish /app/publish .

# Entrypoint
ENTRYPOINT ["dotnet", "Hyperdrive.Identity.Service.dll"]