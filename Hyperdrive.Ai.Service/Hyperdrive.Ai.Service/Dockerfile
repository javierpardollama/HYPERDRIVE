# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app

# Opcional: fija los puertos HTTP/HTTPS explícitamente (coincide con EXPOSE)
ENV ASPNETCORE_HTTP_PORTS=http://+:8090
ENV ASPNETCORE_HTTPS_PORTS=https://+:8091

# Expone sólo los puertos necesarios.
EXPOSE 8090
EXPOSE 8091

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia únicamente archivos de proyecto para aprovechar el caché en el restore
COPY ["Hyperdrive.Ai.Service/Hyperdrive.Ai.Service.csproj", "Hyperdrive.Ai.Service/"]
COPY ["Hyperdrive.Ai.Application/Hyperdrive.Ai.Application.csproj", "Hyperdrive.Ai.Application/"]
COPY ["Hyperdrive.Ai.Domain/Hyperdrive.Ai.Domain.csproj", "Hyperdrive.Ai.Domain/"]
COPY ["Hyperdrive.Ai.Infrastructure/Hyperdrive.Ai.Infrastructure.csproj", "Hyperdrive.Ai.Infrastructure/"]

# Restore del proyecto principal (sin compilar) usando el runtime actual para cache estable
RUN dotnet restore "./Hyperdrive.Service/Hyperdrive.Service.csproj" --locked-mode

# Ahora copia el resto del código (esto invalida el caché sólo si cambian fuentes)
COPY . .

# Compila sin restaurar (ya hicimos restore), salida a una carpeta intermedia
WORKDIR "/src/Hyperdrive.Ai.Service"
RUN dotnet build "./Hyperdrive.Ai.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica sin compilar y sin restaurar, reduciendo tamaño final
RUN dotnet publish "Hyperdrive.Ai.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore --no-build

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app

# Copia artefactos de publish
COPY --from=publish /app/publish .

# Entrypoint
ENTRYPOINT ["dotnet", "Hyperdrive.Ai.Service.dll"]