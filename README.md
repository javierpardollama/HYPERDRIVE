# BONDAGE

Este proyecto surge como una solución para gestionar el estado actual de los archivos reservados, administrando la carga de los archivos suministrados, así como su descarga. 

## ARQUITECTURA

Este Proyecto está construido siguiendo el patrón de N capas:

1. [Data Tier](https://github.com/javierpardollama/BONDAGE/tree/master/Bondage.Portal/Bondage.Tier.Contexts)

En Esta capa la información es almacenada y redistribuida al sistema de almacenamiento de datos.

2. [Logic Tier](https://github.com/javierpardollama/BONDAGE/tree/master/Bondage.Portal/Bondage.Tier.Services)

En esta capa se coordina el envío y/o recepción entre la capa de datos (Data Tier) y la capa de presentación (Presentation Tier). 
Además, toma decisiones lógicas, realiza cálculos y se encarga de procesar órdenes distintas.

3. [Presentation Tier](https://github.com/javierpardollama/BONDAGE/tree/master/Bondage.Portal/Sandwitch.Tier.Web)

En esta capa se traducen las distintas órdenes y resultados a una forma que el usuario pueda comprender.

## BUILD

Para compilar y hacer funcionar este proyecto se recomienda utilizar una serie de herramientas con las cuales este proyecto ha sido construido y probado:

1. [.NET](https://dotnet.microsoft.com/)

Este framework es utilizado para construir todo lo referente a las capas Data Tier y Logic Tier.

2. [Node.js](https://nodejs.org/es/)

Este framework es utilizado para construir el entorno necesario para la capa Presentation Tier.

3. [Angular Cli](https://cli.angular.io/)

Este framework es utilizado para construir todo lo referente a la capa Presentation Tier.

## LICENSE

[MIT](https://github.com/javierpardollama/KINGPIN/blob/master/LICENSE)
