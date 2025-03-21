### **Laboratorio: Database First + Code First con Entity Framework Core**

#### **Paso 1: Configuración del entorno**
1. **Instala las herramientas:**
   - Visual Studio Code.
   - .NET SDK (versión 8.0 o superior, verifica con `dotnet --version`).
   - SQL Server (LocalDB, Express o completo).
   - Extensiones de VS Code: "C#" y "SQL Server (mssql)" (opcional).
   - Herramienta EF Core CLI:
     ```bash
     dotnet tool install --global dotnet-ef
     ```

2. **Actualiza EF Core CLI (recomendado):**
   Para evitar advertencias de versión:
   ```bash
   dotnet tool update --global dotnet-ef
   ```

---

#### **Paso 2: Crear la base de datos (Database First)**
1. **Crea un archivo SQL:**
   En VS Code, crea `create_database.sql`:
   ```sql
   -- Crear la base de datos
   CREATE DATABASE LabDB;
   GO

   -- Usar la base de datos
   USE LabDB;
   GO

   -- Crear la tabla Productos
   CREATE TABLE Productos (
       Id INT PRIMARY KEY IDENTITY(1,1),
       Nombre NVARCHAR(100) NOT NULL,
       Precio DECIMAL(18,2) NOT NULL
   );
   GO

   -- Insertar datos de ejemplo
   INSERT INTO Productos (Nombre, Precio) VALUES
   ('Laptop', 1200.50),
   ('Mouse', 25.99);
   GO
   ```

2. **Ejecuta el script:**
   Usa SQL Server Management Studio (SSMS) o la extensión "SQL Server (mssql)" en VS Code para crear la base de datos y la tabla.

---

#### **Paso 3: Crear el proyecto .NET y generar modelos con Database First**
1. **Crea el proyecto:**
   En la terminal:
   ```bash
   dotnet new console -n LabEFCore
   cd LabEFCore
   ```

2. **Agrega paquetes de EF Core:**
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Design
   ```

3. **Genera modelos desde la base de datos:**
   Usa el comando `scaffold` para crear el contexto y los modelos:
   ```bash
   dotnet ef dbcontext scaffold "Server=LAPTOP-FF6SODJH\SQLEXPRESS01;Database=LabDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
   ```
   - Esto genera:
     - Carpeta `Models`.
     - `Producto.cs` (modelo).
     - `LabDBContext.cs` (contexto).

4. **Revisa los archivos generados:**
   - `Producto.cs`:
     ```csharp
     namespace LabEFCore.Models;
     public partial class Producto
     {
         public int Id { get; set; }
         public string Nombre { get; set; }
         public decimal Precio { get; set; }
     }
     ```
   - `LabDBContext.cs` (ajústalo si es necesario):
     ```csharp
     using Microsoft.EntityFrameworkCore;
     using LabEFCore.Models;

     namespace LabEFCore;
     public partial class LabDBContext : DbContext
     {
         public DbSet<Producto> Productos { get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LabDB;Trusted_Connection=True;");
         }
     }
     ```

5. **Prueba la conexión:**
   Modifica `Program.cs`:
   ```csharp
   using LabEFCore.Models;
   using Microsoft.EntityFrameworkCore;

   class Program
   {
       static void Main(string[] args)
       {
           using (var db = new LabDBContext())
           {
               var productos = db.Productos.ToList();
               foreach (var p in productos)
               {
                   Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Precio: {p.Precio}");
               }
           }
       }
   }
   ```
   Ejecuta:
   ```bash
   dotnet run
   ```
   Deberías ver los datos (`Laptop` y `Mouse`).

---

#### **Paso 4: Configurar migraciones para Code First**
1. **Crea la migración inicial basada en el estado actual:**
   Con el modelo `Producto.cs` sin cambios (sin `Stock`), genera una migración que represente la base de datos existente:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
   - Esto creará un archivo en `Migrations` que define la tabla `Productos` con `Id`, `Nombre` y `Precio`.

2. **Registra la migración sin aplicarla:**
   Como la tabla ya existe, registra `InitialCreate` en la base de datos sin intentar crearla:
   - Ejecuta este SQL en SSMS para crear la tabla `__EFMigrationsHistory` y registrar la migración:
     ```sql
     USE LabDB;
     IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__EFMigrationsHistory')
     BEGIN
         CREATE TABLE [__EFMigrationsHistory] (
             [MigrationId] nvarchar(150) NOT NULL,
             [ProductVersion] nvarchar(32) NOT NULL,
             CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
         );
     END
     INSERT INTO [__EFMigrationsHistory] (MigrationId, ProductVersion)
     VALUES ('20250321020836_InitialCreate', '9.0.3');
     ```
     (Ajusta el `MigrationId` al nombre real generado, como `20250321020836_InitialCreate`, que encontrarás en la carpeta `Migrations`).

---

#### **Paso 5: Modificar el modelo y aplicar migraciones**
1. **Agrega el campo `Stock`:**
   Edita `Producto.cs`:
   ```csharp
   namespace LabEFCore.Models;
   public partial class Producto
   {
       public int Id { get; set; }
       public string Nombre { get; set; }
       public decimal Precio { get; set; }
       public int Stock { get; set; }
   }
   ```

2. **Genera una migración para el cambio:**
   ```bash
   dotnet ef migrations add AddStockToProducto
   ```
   - Abre el archivo generado en `Migrations` (por ejemplo, `20250321020836_AddStockToProducto.cs`) y verifica que contenga:
     ```csharp
     migrationBuilder.AddColumn<int>(
         name: "Stock",
         table: "Productos",
         type: "int",
         nullable: false,
         defaultValue: 0);
     ```

3. **Aplica la migración:**
   ```bash
   dotnet ef database update
   ```
   Esto agregará la columna `Stock` a la tabla `Productos`.

---

#### **Paso 6: Prueba el resultado**
Modifica `Program.cs`:
```csharp
using LabEFCore.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var db = new LabDBContext())
        {
            db.Productos.Add(new Producto { Nombre = "Teclado", Precio = 45.99M, Stock = 10 });
            db.SaveChanges();

            var productos = db.Productos.ToList();
            foreach (var p in productos)
            {
                Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Precio: {p.Precio}, Stock: {p.Stock}");
            }
        }
    }
}
```
Ejecuta:
```bash
dotnet run
```

---

#### **Paso 7: Revertir los cambios (opcional)**
1. **Revierte la migración:**
   Para eliminar la columna `Stock`:
   ```bash
   dotnet ef database update InitialCreate
   ```

2. **Elimina la migración `AddStockToProducto`:**
   ```bash
   dotnet ef migrations remove
   ```

---

### **Notas importantes**
- **Cadena de conexión:** Asegúrate de que `"Server=LAPTOP-FF6SODJH\SQLEXPRESS01;Database=LabDB;Trusted_Connection=True;TrustServerCertificate=True;"` sea correcta para tu entorno.
- **Historial de migraciones:** Si algo falla, verifica `__EFMigrationsHistory` con:
  ```sql
  SELECT * FROM __EFMigrationsHistory;
  ```
- **Evitar errores:** El paso clave es registrar manualmente `InitialCreate` para que EF Core no intente recrear la tabla existente.

---

