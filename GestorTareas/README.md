# 📋 Gestor de Tareas Personales

Una aplicación web completa para la gestión personal de tareas, desarrollada con ASP.NET Core y Razor Pages.

## 🚀 Características

### Gestión de Tareas
- ✅ Crear, editar, eliminar y ver tareas
- 📅 Fechas de vencimiento con alertas
- 🎯 Sistema de prioridades (Baja, Media, Alta, Urgente)
- 📂 Organización por categorías
- 🏷️ Sistema de etiquetas
- ⏱️ Estimación de tiempo
- 📝 Notas adicionales y URLs de referencia

### Funcionalidades Avanzadas
- 🔍 Búsqueda y filtrado avanzado
- 📊 Dashboard con estadísticas
- 🔔 Notificaciones de tareas vencidas
- 💾 Autoguardado y sincronización
- 📱 Interfaz responsive

### Seguridad
- 🔐 Autenticación robusta con ASP.NET Identity
- 🛡️ Headers de seguridad HTTP
- 🔒 Validaciones del lado servidor y cliente
- 👤 Aislamiento completo entre usuarios

## 🛠️ Tecnologías Utilizadas

- **Backend**: ASP.NET Core 8.0, Razor Pages
- **Base de Datos**: SQL Server con Entity Framework Core
- **Frontend**: Bootstrap 5, JavaScript ES6+, Font Awesome
- **Autenticación**: ASP.NET Core Identity
- **Testing**: xUnit, FluentAssertions
- **Caché**: MemoryCache

## 📦 Instalación

### Prerrequisitos
- .NET 8.0 SDK
- SQL Server LocalDB (incluido con Visual Studio)
- Visual Studio 2022 o VS Code

### Pasos de Instalación

1. **Clonar el repositorio**
   ```bash
   git clone [URL_DEL_REPOSITORIO]
   cd GestorTareas

   1. **Restaurar paquetes**

   ```bash
   dotnet restore
   ```

2. **Configurar base de datos**

   ```bash
   dotnet ef database update
   ```

3. **Ejecutar la aplicación**

   ```bash
   dotnet run
   ```

4. **Abrir navegador**

   ```
   https://localhost:5001
   ```

## 