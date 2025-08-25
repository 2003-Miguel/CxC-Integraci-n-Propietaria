# Cuentas por Cobrar
Proyecto ASP.NET Core MVC para gestión de Cuentas Por Cobrar y consultas a servicios externos (Tasa de Cambio, Inflación, Salud Financiera, Historial Crediticio).

## Características

- Consulta dinámica de tasa de cambio para varias monedas.
- Consulta del índice de inflación con selección por mes y año.
- Consulta de salud financiera y visualización amigable.
- Consulta y visualización de historial crediticio en formato tabla.
- Navegación con menú intuitivo usando Bootstrap.

## Requisitos

- .NET 6 o superior
- SQL Server para la base de datos
- Visual Studio 2022 o Visual Studio Code

## Cómo usar

1. Clona este repositorio.
2. Restaura los paquetes NuGet: `dotnet restore`.
3. Configura la cadena de conexión en `appsettings.json`.
4. Restaura y ejecuta la base de datos con el respaldo ubicado en `/Database/CxcDB.bak`.
5. Ejecuta la aplicación.

## Base de datos

El respaldo de la base de datos está en la carpeta `/Database/` con el nombre `CxcDB.bak`. Restaura este respaldo en tu instancia local de SQL Server para probar la aplicación.

