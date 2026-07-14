# HYPERDRIVE

This project arose as a solution for managing the current state of reserved files, administering the uploading and downloading of supplied files.

## ARCHITECTURE

This project is constructed using a micro-services oriented architecture:

- IDENTITY

[![Identity - Test .NET Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-identity-infrastructure.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-identity-infrastructure.yml) 
[![Identity - Build .NET Service](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-identity-service.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-identity-service.yml) 

.NET MVC Api. In this service, operations related to authentication and security are performed.

- STORAGE

[![Storage - Test .NET Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-storage-infrastructure.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-storage-infrastructure.yml) 
[![Storage - Build .NET Service](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-storage-service.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-storage-service.yml) 

.NET MVC Api. In this service, operations related to file system are performed

- INTELLIGENCE

[![Intelligence - Build .NET Service](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-Intelligence-service.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/dotnet-Intelligence-service.yml)

.NET MVC Api. In this service, operations related to Artificial Intelligence are performed

- CLIENT

[![Build Angular App](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml/badge.svg)](https://github.com/javierpardollama/HYPERDRIVE/actions/workflows/node.js.yml)

Angular Web Application. Provides a unified User Interface to perform all the operations.

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
