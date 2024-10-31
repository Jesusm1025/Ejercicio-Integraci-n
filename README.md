
# ApiUsuario

Esta es una API RESTful construida en .NET para la gestión de usuarios. La API permite registrar usuarios, validando el formato de correo electrónico y la contraseña según expresiones regulares. También incluye la funcionalidad para agregar y listar teléfonos asociados a cada usuario.

## Requisitos

- .NET 6+
- SQL Server (o puedes usar otro sistema compatible como MySQL o PostgreSQL)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Instalación y Configuración

### Configurar la Base de Datos

1. Crea una base de datos en SQL Server para este proyecto (por ejemplo, `UserDb`).
2. Usa el script `DatabaseScript.sql` en el repositorio para crear las tablas necesarias:
   - Abre el script en SQL Server Management Studio (SSMS) o en tu entorno de SQL.
   - Ejecuta el script para crear la estructura de la base de datos.

### Configurar la Cadena de Conexión

1. En el archivo `appsettings.json`, actualiza la cadena de conexión a tu configuración local:
   ```json
   "ConnectionStrings": {
     "Connection": "Server=DESARROLLO01;Database=UserDb;Trusted_Connection=true;TrustServerCertificate=true"
   }
   ```

### Configurar Expresión Regular para Contraseñas

El proyecto permite configurar la expresión regular para las contraseñas a través del archivo `appsettings.json`. Asegúrate de que la expresión esté configurada de acuerdo con tus requisitos de seguridad.

Ejemplo en `appsettings.json`:

```json
"PasswordRegex": "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$"
```

### Ejecutar la Aplicación

1. Restaura las dependencias del proyecto:
   ```bash
   dotnet restore
   ```

2. Compila y ejecuta la aplicación:
   ```bash
   dotnet run
   ```

La aplicación debería estar disponible en `https://localhost:5001` o `http://localhost:5000` según la configuración predeterminada.

## Endpoints

### Crear un Usuario

- **Endpoint**: `POST /api/users`
- **Descripción**: Crea un nuevo usuario con nombre, correo, contraseña y lista de teléfonos.

**Formato de solicitud**:
```json
{
  "name": "Juan Rodriguez",
  "email": "juan@rodriguez.org",
  "password": "hunter2",
  "phones": [
    {
      "number": "1234567",
      "cityCode": "1",
      "countryCode": "57"
    }
  ]
}
```

**Formato de respuesta en caso de éxito**:
```json
{
  "id": "uuid",
  "name": "Juan Rodriguez",
  "email": "juan@rodriguez.org",
  "created": "2024-10-31T17:24:57.463Z",
  "modified": "2024-10-31T17:24:57.463Z",
  "lastLogin": "2024-10-31T17:24:57.463Z",
  "token": "uuid",
  "isActive": true,
  "phones": [
    {
      "number": "1234567",
      "cityCode": "1",
      "countryCode": "57"
    }
  ]
}
```

**Formato de respuesta en caso de error**:
```json
{
  "mensaje": "El correo ya registrado" // o "El correo no tiene un formato válido", según el caso
}
```

### Obtener un Usuario por ID

- **Endpoint**: `GET /api/users/{id}`
- **Descripción**: Devuelve los datos de un usuario específico.

### Actualizar un Usuario

- **Endpoint**: `PUT /api/users/{id}`
- **Descripción**: Actualiza los datos de un usuario específico.

### Eliminar un Usuario

- **Endpoint**: `DELETE /api/users/{id}`
- **Descripción**: Elimina un usuario específico.

## Mensajes de Error

Todos los errores son devueltos en el formato JSON:

```json
{
  "mensaje": "mensaje de error"
}
```

## Diagrama de la Solución

(Agrega aquí un enlace o una imagen del diagrama de la solución si tienes uno)

## Autor

- **Tu Nombre** - [tu-usuario en GitHub](https://github.com/tu-usuario)
