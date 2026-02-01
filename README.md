# DenunciasWebApp

Sistema web para registro y seguimiento de denuncias ciudadanas desarrollado con ASP.NET Core 8.0 MVC.

## Descripcion

Esta aplicacion permite a los usuarios registrar denuncias que son gestionadas por administradores. Los usuarios pueden crear, editar y eliminar sus propias denuncias, mientras que los administradores pueden ver todas las denuncias y cambiar su estado (Activa, En Proceso, Finalizada).

## Tecnologias

- ASP.NET Core 8.0 MVC
- Entity Framework Core
- SQLite
- ASP.NET Core Identity
- Bulma CSS

## Requisitos

- .NET 8.0 SDK
- Entity Framework Core Tools (para migraciones)

Para instalar EF Core Tools:

```bash
dotnet tool install --global dotnet-ef
```

## Instalacion

1. Clonar el repositorio:

```bash
git clone <url-del-repositorio>
cd DenunciasWebApp
```

2. Restaurar dependencias:

```bash
dotnet restore
dotnet tool restore
```

3. Aplicar migraciones para crear la base de datos:

```bash
dotnet ef database update
```

4. Ejecutar la aplicacion:

```bash
dotnet run
```

Para desarrollo con hot reload:

```bash
dotnet watch run
```

## URLs de acceso

- HTTP: http://localhost:5235

## Usuario Administrador

Al iniciar la aplicacion por primera vez, se crea automaticamente un usuario administrador con las siguientes credenciales:

- **Email:** admin@admin.com
- **Password:** Admin123!

Este usuario tiene el rol "Administrador" y puede acceder al panel de Atencion Ciudadana para gestionar todas las denuncias.

## Roles

La aplicacion maneja dos roles:

- **Usuario:** Puede crear, ver, editar y eliminar sus propias denuncias.
- **Administrador:** Puede ver todas las denuncias y cambiar su estado.

Los roles se crean automaticamente al iniciar la aplicacion. Los nuevos usuarios registrados reciben automaticamente el rol "Usuario".

## Estructura del proyecto

```
DenunciasWebApp/
├── Areas/Identity/          # Paginas de autenticacion (Login, Register)
├── Controllers/
│   ├── HomeController.cs    # Pagina principal
│   ├── ComplaintsController.cs  # CRUD de denuncias para usuarios
│   └── AdminController.cs   # Panel de administracion
├── Data/
│   └── ApplicationDbContext.cs  # Contexto de Entity Framework
├── Models/
│   ├── Complaint.cs         # Modelo de denuncia
│   └── ApplicationUser.cs   # Usuario extendido de Identity
├── Views/
│   ├── Home/
│   ├── Complaints/
│   ├── Admin/
│   └── Shared/
├── Migrations/              # Migraciones de EF Core
└── app.db                   # Base de datos SQLite (generada)
```

## Comandos utiles

```bash
# Compilar el proyecto
dotnet build

# Ejecutar sin hot reload
dotnet run

# Ejecutar con hot reload
dotnet watch run

# Crear una nueva migracion
dotnet ef migrations add NombreMigracion

# Aplicar migraciones pendientes
dotnet ef database update

# Revertir ultima migracion
dotnet ef database update PreviousMigrationName

# Ver migraciones aplicadas
dotnet ef migrations list
```

## Estados de denuncia

- **Active (Activa):** Estado inicial cuando se crea una denuncia
- **Processing (En Proceso):** La denuncia esta siendo atendida
- **Done (Finalizada):** La denuncia ha sido resuelta
