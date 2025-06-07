// DESARROLLO

using GestorTareas.Data;
using GestorTareas.Middleware;
using GestorTareas.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddRazorPages();

// Añadir servicios al contenedor
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Identity mejorada
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // Configuración de contraseñas
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 3;

    // Configuración de bloqueo de cuenta
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configuración de usuario
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    // Configuración de inicio de sesión
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<AppDbContext>();

// Servicios personalizados
builder.Services.AddScoped<ITareaValidationService, TareaValidationService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IBusquedaService, BusquedaService>();
// Configuración de Razor Pages
builder.Services.AddRazorPages(options =>
{
    // Requerir autorización para todas las páginas de tareas
    options.Conventions.AuthorizeFolder("/Tareas");  // ← Usar AuthorizeFolder en lugar de AuthorizeAreaFolder

    // Configurar anti-forgery
    options.Conventions.ConfigureFilter(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
});

// Configuración de anti-forgery
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
    options.SuppressXFrameOptionsHeader = false;
});

// Configuración de HSTS
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(60);
});


var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-ES"),
    SupportedCultures = new[] { new System.Globalization.CultureInfo("es-ES") },
    SupportedUICultures = new[] { new System.Globalization.CultureInfo("es-ES") }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Middleware de seguridad personalizado
app.UseMiddleware<SecurityHeadersMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

// PRODUCCCIÓN
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using GestorTareas.Data;
// using GestorTareas.Services;
// using GestorTareas.Middleware;

// var builder = WebApplication.CreateBuilder(args);

// // ===== CONFIGURACIÓN DE LOGGING =====
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

// if (builder.Environment.IsProduction())
// {
//     builder.Logging.AddEventLog();
//     builder.Logging.SetMinimumLevel(LogLevel.Warning);
// }
// else
// {
//     builder.Logging.AddDebug();
//     builder.Logging.SetMinimumLevel(LogLevel.Information);
// }

// // ===== BASE DE DATOS =====
// builder.Services.AddDbContext<AppDbContext>(options =>
// {
//     var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//     options.UseSqlServer(connectionString);
    
//     // Configuraciones para producción
//     if (builder.Environment.IsProduction())
//     {
//         options.EnableSensitiveDataLogging(false);
//         options.EnableDetailedErrors(false);
//     }
//     else
//     {
//         options.EnableSensitiveDataLogging(true);
//         options.EnableDetailedErrors(true);
//     }
// });

// // ===== IDENTITY =====
// builder.Services.AddDefaultIdentity<IdentityUser>(options => {
//     // Configuración de contraseñas
//     options.Password.RequireDigit = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireUppercase = true;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequiredLength = 8;
//     options.Password.RequiredUniqueChars = 3;

//     // Configuración de bloqueo
//     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
//     options.Lockout.MaxFailedAccessAttempts = 5;
//     options.Lockout.AllowedForNewUsers = true;

//     // Configuración de usuario
//     options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//     options.User.RequireUniqueEmail = true;

//     // Configuración de confirmación
//     options.SignIn.RequireConfirmedAccount = builder.Environment.IsProduction();
//     options.SignIn.RequireConfirmedEmail = builder.Environment.IsProduction();
// })
// .AddEntityFrameworkStores<AppDbContext>();

// // ===== SERVICIOS PERSONALIZADOS =====
// builder.Services.AddScoped<ITareaValidationService, TareaValidationService>();
// builder.Services.AddScoped<ICategoriaService, CategoriaService>();
// builder.Services.AddScoped<IBusquedaService, BusquedaService>();
// builder.Services.AddScoped<ICacheService, CacheService>();

// // ===== CACHÉ =====
// builder.Services.AddMemoryCache();

// // ===== CONFIGURACIÓN DE RAZOR PAGES =====
// builder.Services.AddRazorPages(options =>
// {
//     // options.Conventions.AuthorizeFolder("", "/Tareas");
//     options.Conventions.AuthorizeFolder("/Tareas"); 
//     options.Conventions.ConfigureFilter(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
// });

// // ===== CONFIGURACIÓN DE ANTIFORGERY =====
// builder.Services.AddAntiforgery(options =>
// {
//     options.HeaderName = "RequestVerificationToken";
//     options.SuppressXFrameOptionsHeader = false;
    
//     // Configuración diferente para desarrollo vs producción
//     if (builder.Environment.IsDevelopment())
//     {
//         options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;  // ← Permite HTTP en desarrollo
//         options.Cookie.SameSite = SameSiteMode.Lax;  // ← Más permisivo en desarrollo
//     }
//     else
//     {
//         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // ← HTTPS obligatorio en producción
//         options.Cookie.SameSite = SameSiteMode.Strict;  // ← Estricto en producción
//     }
// });

// // ===== CONFIGURACIÓN DE HSTS =====
// if (builder.Environment.IsProduction())
// {
//     builder.Services.AddHsts(options =>
//     {
//         options.Preload = true;
//         options.IncludeSubDomains = true;
//         options.MaxAge = TimeSpan.FromDays(365);
//     });
// }

// // ===== CONFIGURACIÓN DE COMPRESIÓN =====
// builder.Services.AddResponseCompression(options =>
// {
//     options.EnableForHttps = true;
// });

// var app = builder.Build();

// // ===== PIPELINE DE MIDDLEWARE =====

// // Middleware de performance (solo en desarrollo)
// if (app.Environment.IsDevelopment())
// {
//     // app.UseMiddleware<PerformanceMiddleware>();
// }

// // Manejo de errores
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     app.UseHsts();
// }

// // Compresión
// app.UseResponseCompression();

// // Middleware de seguridad
// app.UseMiddleware<SecurityHeadersMiddleware>();

// // Redirección HTTPS
// app.UseHttpsRedirection();

// // Archivos estáticos con caché
// app.UseStaticFiles(new StaticFileOptions
// {
//     OnPrepareResponse = ctx =>
//     {
//         if (app.Environment.IsProduction())
//         {
//             ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000");
//         }
//     }
// });

// app.UseRouting();

// // Localización
// app.UseRequestLocalization(new RequestLocalizationOptions
// {
//     DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-ES"),
//     SupportedCultures = new[] { new System.Globalization.CultureInfo("es-ES") },
//     SupportedUICultures = new[] { new System.Globalization.CultureInfo("es-ES") }
// });

// app.UseAuthentication();
// app.UseAuthorization();

// app.MapRazorPages();

// // ===== INICIALIZACIÓN DE BASE DE DATOS =====
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
//     if (app.Environment.IsProduction())
//     {
//         // En producción, solo migrar
//         context.Database.Migrate();
//     }
//     else
//     {
//         // En desarrollo, recrear si es necesario
//         context.Database.EnsureCreated();
//     }
// }

// app.Run();