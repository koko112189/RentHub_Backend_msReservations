# Applicant System Backend

Esta es una aplicación ASP.NET Core diseñada para gestionar candidatos, permitiendo guardar, cargar y eliminar registros. La aplicación utiliza autenticación mediante **JWT (JSON Web Token)** y una base de datos SQL Server.

---

## **Requisitos previos**
Antes de desplegar la aplicación, asegúrate de contar con lo siguiente:
- **SDK .NET Core 6.0** o superior instalado.
- **SQL Server** configurado y en funcionamiento.
- Un editor de texto o IDE como **Visual Studio** o **Visual Studio Code**.
- **Postman** o cualquier herramienta para probar los endpoints (opcional)

---

## **Configuración inicial**

1. **Clona el repositorio**:
   ```bash
   git clone https://github.com/koko112189/applicant_system_backend.git
   cd applicant_system_backend

2. **Configura el archivo appsettings.json**

   Asegúrate de que el archivo appsettings.json incluya las configuraciones correctas. Un ejemplo:
  
   ```
   {
    "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
    },
    "AllowedHosts": "*",
    "JwtSettings": {
          "SecretKey": "my_super_secret_key_that_is_long_enough",
          "Issuer": "your_issuer",
          "Audience": "your_audience"
    },
    "ConnectionStrings": {
          "DefaultConnection": "Server=localhost;Database=ApplicationSystemDB;User Id=sa;Password=test1234;Trust Server       Certificate=true;"
      }
    }

Parámetros importantes:

JwtSettings.SecretKey: Cambia este valor por una clave secreta segura para JWT.
ConnectionStrings.DefaultConnection: Configura la conexión a tu base de datos SQL Server

3. **Configura las migraciones de la base de datos**
   Ejecuta los siguientes comandos en la raíz del proyecto para asegurarte de que la base de datos esté configurada:

    ```bash
    dotnet ef database update

  ## **Despliegue local**
  ```bash
  dotnet build
  dotnet run --project ApplicantsAPI
  ```

# Autenticación JWT
La aplicación utiliza autenticación mediante JWT. Para acceder a los endpoints protegidos:

Realiza una solicitud de inicio de sesión al endpoint de autenticación /api/auth/login con las credenciales del usuario que 
se creó en api/auth/create.
Recibirás un token JWT que debes incluir en el encabezado de las solicitudes en este formato

```
Authorization: Bearer <TOKEN>
```

**Estructura del proyecto**
La estructura del proyecto es una arquitectura basada en principios de capas (Layered Architecture). A continuación, se describe cada sección:

1. ApplicantsAPI
Este es el proyecto principal de la API (Web API) y contiene:
Controllers: Controladores de la API para gestionar las solicitudes HTTP y exponer endpoints.
Middlewares: Clases para manejar la lógica del pipeline de solicitudes/respuestas HTTP.
Migrations: Migraciones para gestionar cambios en la base de datos usando Entity Framework.
Program.cs: Configuración principal del host de la aplicación.
appsettings.json: Configuración de la aplicación, como cadenas de conexión o valores clave.

3. Application
Contiene la lógica de negocio y las reglas del dominio:
Dependencias (DTOs): Clases de transferencia de datos utilizadas para mover datos entre capas.
Interfaces: Definición de contratos que las clases deben implementar (por ejemplo, repositorios o servicios).
Mapping: Configuración de mapeos entre entidades y DTOs, probablemente usando AutoMapper.
Services: Servicios que implementan la lógica de negocio principal (e.g., manejo de aplicaciones o autenticación).
3. ApplicationTests
Proyecto para pruebas unitarias y de integración asociado a la capa de aplicación.

4. Domain
Define el núcleo del dominio y contiene:
Entities: Clases que representan las entidades del dominio (e.g., Applicant).
Exceptions: Clases personalizadas para manejar excepciones específicas del dominio.
ValueObjects: Objetos inmutables que encapsulan conceptos del dominio.
5. DomainTests
Proyecto de pruebas unitarias específico para la lógica del dominio.

6. Infrastructure
Contiene dependencias de infraestructura relacionadas con la persistencia y configuraciones:
Persistence
Gestión de la persistencia de datos:
ApplicantsDbContext.cs: Contexto de base de datos de Entity Framework Core.
UnitOfWork.cs: Implementación del patrón Unidad de Trabajo (Unit of Work), para agrupar operaciones transaccionales.
Repositories: Repositorios que gestionan la lógica de acceso a datos para cada entidad.
Resumen

Este proyecto sigue la arquitectura Clean Architecture o Hexagonal Architecture, donde:
El dominio está en el núcleo, conteniendo reglas de negocio y modelos.
La aplicación implementa la lógica de casos de uso y orquesta el trabajo entre capas.
La infraestructura maneja la persistencia y configuraciones específicas.
La API actúa como una interfaz de entrada para los usuarios.
Esto asegura modularidad, separación de responsabilidades y un diseño escalable.





