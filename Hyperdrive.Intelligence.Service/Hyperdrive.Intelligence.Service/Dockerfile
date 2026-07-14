# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# Usa usuario no root
USER $APP_UID

# Opcional: fija los puertos HTTP/HTTPS expl�citamente (coincide con EXPOSE)
ENV ASPNETCORE_URLS=http://+:8090;https://+:8091

# Expone sólo los puertos necesarios.
EXPOSE 8090
EXPOSE 8091

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia únicamente archivos de proyecto para aprovechar el caché en el restore
COPY ["Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Service.csproj", "Hyperdrive.Intelligence.Service/"]
COPY ["Hyperdrive.Intelligence.Application/Hyperdrive.Intelligence.Application.csproj", "Hyperdrive.Intelligence.Application/"]
COPY ["Hyperdrive.Intelligence.Domain/Hyperdrive.Intelligence.Domain.csproj", "Hyperdrive.Intelligence.Domain/"]
COPY ["Hyperdrive.Intelligence.Infrastructure/Hyperdrive.Intelligence.Infrastructure.csproj", "Hyperdrive.Intelligence.Infrastructure/"]
COPY ["Hyperdrive.Ai/nuget.config", "./nuget.config"]

# Restore del proyecto principal (sin compilar) usando el runtime actual para cache estable
RUN dotnet restore "./Hyperdrive.Intelligence.Service/Hyperdrive.Intelligence.Service.csproj" --configfile "./nuget.config" --locked-mode

# Ahora copia el resto del código (esto invalida el caché sólo si cambian fuentes)
COPY . .

# Compila sin restaurar (ya hicimos restore), salida a una carpeta intermedia
WORKDIR "/src/Hyperdrive.Intelligence.Service"
RUN dotnet build "./Hyperdrive.Intelligence.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica sin compilar y sin restaurar, reduciendo tamaño final
RUN dotnet publish "Hyperdrive.Intelligence.Service.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish 

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app

# Copia artefactos de publish
COPY --from=publish /app/publish .

# Entrypoint
ENTRYPOINT ["dotnet", "Hyperdrive.Intelligence.Service.dll"]