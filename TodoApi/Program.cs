var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Configurar Kestrel para solo HTTP
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5185); // Escucha en cualquier IP, puerto 5185 (HTTP)
});

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
