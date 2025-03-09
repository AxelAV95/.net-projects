var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// app.UseHttpRedirection(); // Cambia de UseHttpsRedirection a UseHttpRedirection
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();