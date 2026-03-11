# HYPERDRIVE

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white) ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) ![Angular](https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white) ![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white) ![MongoDB](https://img.shields.io/badge/MongoDB-%234ea94b.svg?style=for-the-badge&logo=mongodb&logoColor=white)

[![Main - Test .NET Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-main-infrastructure.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-main-infrastructure.yml) 
[![Main - Build .NET Service](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-main-service.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-main-service.yml) 
[![Ai - Build .NET Service](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-ai-service.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-ai-service.yml) 
[![Build Angular App](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml) 
![GitHub License](https://img.shields.io/github/license/javierpardollama/HYPERDRIVE)


This project arose as a solution for managing the current state of reserved files, administering the uploading and downloading of supplied files.

## ARCHITECTURE

This project is built in n layers, following a hexagonal structure (ports - adapters), under a domain-driven design:

1. [Domain](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Domain)

In this layer, business rules are modeled and entities, value objects, etc., are defined.

2. [Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Infrastructure)

In this layer, information is stored and redistributed to the data storage system.

3. [Application](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Application)

This layer coordinates the sending and/or receiving of data between the Domain layer and the Infrastructure layer.

## GETTING STARTED

To compile and run this project, it is recommended to use the following tools, which were used to build and test this project:

1. [.NET](https://dotnet.microsoft.com/)

This framework is used to build everything related to the Service: the Domain layer, the Infrastructure layer, and the Application layer.

2. [Docker Desktop](https://www.docker.com/products/docker-desktop/)

This platform is used to package the Service.

3. [Node.js](https://nodejs.org/es/)

This runtime environment is used to build the necessary environment for the Presentation layer.

4. [Angular CLI](https://cli.angular.io/)

This framework is used to build everything related to the Presentation layer.


## CONTRIBUTING

If you are interested in reporting/fixing issues and contributing directly to the code base, please see [CONTRIBUTING.md](https://github.com/javierpardollama/HYPERDRIVE/blob/main/CONTRIBUTING.md) for more information on what we're looking for and how to get started.


## LICENSE

[MIT](https://github.com/javierpardollama/HYPERDRIVE/blob/main/LICENSE)
