# HYPERDRIVE

Este proyecto surge como una solución para gestionar el estado actual de los archivos reservados, administrando la carga de los archivos suministrados, así como su descarga. 

## ARQUITECTURA

Este Proyecto está construido en capas siguiendo el diseño guiado por dominio:

1. [Domain](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Domain)

En esta capa se modelan las reglas de negocio y se definen las entidades, objetos de valor, etc.

2. [Infrastructure](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Infrastructure)

En esta capa la información es almacenada y redistribuida al sistema de almacenamiento de datos.

3. [Application](https://github.com/javierpardollama/HYPERDRIVE/tree/main/Hyperdrive.Service/Hyperdrive.Application)

En esta capa se coordina el envío y/o recepción entre la capa de dominio (Domain) y la capa de infrastructura (Infrastrucure).


## BUILD

Para compilar y hacer funcionar este proyecto se recomienda utilizar una serie de herramientas con las cuales este proyecto ha sido construido y probado:

1. [.NET](https://dotnet.microsoft.com/)

Este framework es utilizado para construir todo lo referente a las capas Data Tier y Logic Tier.

2. [Node.js](https://nodejs.org/es/)

Este framework es utilizado para construir el entorno necesario para la capa Presentation Tier.

3. [Angular Cli](https://cli.angular.io/)

Este framework es utilizado para construir todo lo referente a la capa Presentation Tier.

## LICENSE

[MIT](https://github.com/javierpardollama/HYPERDRIVE/blob/master/LICENSE)
