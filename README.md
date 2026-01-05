# HYPERDRIVE

[![Test .NET Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-infrastructure.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-infrastructure.yml) [![Build .NET Service](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-service.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-service.yml) [![Build Angular App](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)]([https://github.com/coverlet-coverage/coverlet/blob/master/LICENSE](https://github.com/javierpardollama/HYPERDRIVE/blob/main/LICENSE))

This project arose as a solution for managing the current state of reserved files, administering the uploading and downloading of supplied files.

## ARCHITECTURE

This project is built in n layers, following a hexagonal structure (ports - adapters), under a domain-driven design:

1. [Domain](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Domain)

In this layer, business rules are modeled and entities, value objects, etc., are defined.

2. [Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Infrastructure)

In this layer, information is stored and redistributed to the data storage system.

3. [Application](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Application)

This layer coordinates the sending and/or receiving of data between the Domain layer and the Infrastructure layer.

## BUILD

To compile and run this project, it is recommended to use the following tools, which were used to build and test this project:

1. [.NET](https://dotnet.microsoft.com/)

This framework is used to build everything related to the Service: the Domain layer, the Infrastructure layer, and the Application layer.

2. [Docker Desktop](https://www.docker.com/products/docker-desktop/)

This platform is used to package the Service.

3. [Node.js](https://nodejs.org/es/)

This runtime environment is used to build the necessary environment for the Presentation layer.

4. [Angular CLI](https://cli.angular.io/)

This framework is used to build everything related to the Presentation layer.

## RUN

This project includes a "docker-compose" file. Configure it to be your startup project (in your preferred IDE) and run it without worry.

## LICENSE

[MIT](https://github.com/javierpardollama/HYPERDRIVE/blob/master/LICENSE)
