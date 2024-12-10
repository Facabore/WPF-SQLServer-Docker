# ProductHub WPF

**ProductHub** es una aplicación de escritorio desarrollada en **WPF (Windows Presentation Foundation)** que implementa los principios de **Clean Architecture**. Esta aplicación permite gestionar productos de manera eficiente mediante operaciones CRUD (Crear, Leer, Actualizar y Eliminar). Además, utiliza **SQL Server** en un contenedor Docker como base de datos, garantizando una configuración rápida y consistente para el almacenamiento de datos.

---

## Características

- **Gestión completa de productos:**
  - Crear productos con información básica como nombre, descripción, precio y stock.
  - Leer todos los productos en un formato visual amigable (`DataGrid`).
  - Actualizar productos existentes de manera sencilla.
  - Eliminar productos de forma segura.

- **Clean Architecture:**
  - Separación clara de responsabilidades en capas: Abstracciones, Entidades, Servicios, Infraestructura y Controladores.
  - Fácil de mantener, escalar y extender.

- **Base de datos SQL Server en Docker:**
  - Configuración rápida mediante un contenedor Docker.
  - Garantiza portabilidad y uniformidad en los entornos de desarrollo y producción.

- **Interfaz de usuario intuitiva:**
  - Construida en WPF para proporcionar una experiencia de usuario moderna y funcional.
  - Ventanas emergentes (`MessageBox`) para notificaciones y manejo de errores.

---

## Requisitos Previos

### Herramientas
1. **.NET 6.0 o superior:** Para compilar y ejecutar la aplicación.
2. **Docker Desktop:** Para ejecutar la base de datos SQL Server.
3. **SQL Server Management Studio (opcional):** Para inspeccionar los datos directamente en la base de datos.

### Paquetes NuGet
Asegúrate de instalar los siguientes paquetes en tu proyecto:
- **Microsoft.EntityFrameworkCore**
- **Microsoft.EntityFrameworkCore.SqlServer**
- **Microsoft.Extensions.DependencyInjection**
- **Microsoft.Extensions.Configuration.Json**

---

## Configuración de la Base de Datos con Docker

La aplicación utiliza un contenedor Docker para SQL Server. A continuación, se describe cómo configurarlo:

1. **Instalar Docker Desktop**  
   Si no tienes Docker instalado, descárgalo e instálalo desde [Docker Desktop](https://www.docker.com/products/docker-desktop).

2. **Iniciar el contenedor de SQL Server**  
   Ejecuta el siguiente comando en tu terminal para iniciar un contenedor de SQL Server:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong#Password" -p 1433:1433 --name ProductHub -d mcr.microsoft.com/mssql/server:2022-latest
3. **Abrir el proyecto WPF-SQLSERVER.SLN**
   Has el build del programa, usar
