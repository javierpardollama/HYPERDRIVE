# HYPERDRIVE

[![Test .NET Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-infrastructure.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-infrastructure.yml) [![Build .NET Service](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-service.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-service.yml) [![Build Angular App](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml)

Este proyecto surge como una solución para gestionar el estado actual de los archivos reservados, administrando la carga de los archivos suministrados, así como su descarga. 

## ARQUITECTURA

Este Proyecto está construido en capas siguiendo el diseño guiado por dominio:

1. [Domain](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Domain)

En esta capa se modelan las reglas de negocio y se definen las entidades, objetos de valor, etc.

2. [Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Infrastructure)

En esta capa la información es almacenada y redistribuida al sistema de almacenamiento de datos.

3. [Application](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Application)

En esta capa se coordina el envío y/o recepción entre la capa de dominio (Domain) y la capa de infrastructura (Infrastructure).

## BUILD

Para compilar y hacer funcionar este proyecto se recomienda utilizar una serie de herramientas con las cuales este proyecto ha sido construido y probado:

1. [.NET](https://dotnet.microsoft.com/)

Este framework es utilizado para construir todo lo referente al Servicio: La capas Dominio, la capa de Infrastructura y la capa de Aplicación.

1. [Docker Desktop](https://www.docker.com/products/docker-desktop/)

Esta plataforma es utilizada para empaquetar el Servicio.

2. [Node.js](https://nodejs.org/es/)

Este entorno de ejecución es utilizado para construir el entorno necesario para la capa de Presentación.

3. [Angular Cli](https://cli.angular.io/)

Este framework es utilizado para construir todo lo referente a la capa de Presentación.

## RUN

Este proyecto incluye un fichero "docker-compose". Configúralo para que sea tu proyecto de arranque (en el IDE que prefieras) y ejecútalo sin miedo.

## LICENSE

[MIT](https://github.com/javierpardollama/HYPERDRIVE/blob/master/LICENSE)
