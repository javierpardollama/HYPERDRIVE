# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

# Opcional: fija los puerto HTTP/HTTPS explícitamente (coincide con EXPOSE)
ENV ASPNETCORE_HTTP_PORTS=http://+:8080
ENV ASPNETCORE_HTTPS_PORTS=http://+:8081

# Expone sólo los puertos necesarios.
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia únicamente archivos de proyecto para aprovechar el caché en el restore
COPY ["Hyperdrive.Service/Hyperdrive.Service.csproj", "Hyperdrive.Service/"]
COPY ["Hyperdrive.Application/Hyperdrive.Application.csproj", "Hyperdrive.Application/"]
COPY ["Hyperdrive.Domain/Hyperdrive.Domain.csproj", "Hyperdrive.Domain/"]
COPY ["Hyperdrive.Infrastructure/Hyperdrive.Infrastructure.csproj", "Hyperdrive.Infrastructure/"]

# Restore del proyecto principal (sin compilar) usando el runtime actual para cache estable
RUN dotnet restore "./Hyperdrive.Service/Hyperdrive.Service.csproj" --use-current-runtime

# Ahora copia el resto del código (esto invalida el caché sólo si cambian fuentes)
COPY . .

# Compila sin restaurar (ya hicimos restore), salida a una carpeta intermedia
WORKDIR "/src/Hyperdrive.Service"
RUN dotnet build "./Hyperdrive.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica sin AppHost y sin restaurar, reduciendo tamaño final
RUN dotnet publish "Hyperdrive.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore /p:UseAppHost=false /p:PublishReadyToRun=true

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app

# Copia artefactos de publish
COPY --from=publish /app/publish .

# Entrypoint
ENTRYPOINT ["dotnet", "Hyperdrive.Service.dll"]