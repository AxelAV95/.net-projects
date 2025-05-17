using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);
// builder.Services.AddMediatR(typeof(SomeCommandHandler).Assembly); // Placeholder para comandos después

// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// JWT Config aquí después
// FluentValidation, AutoMapper, MediatR también aquí después


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware básico de sanitización (libería recomendada: Ganss.XSS si se requiere después)
app.UseHttpsRedirection();
app.UseCors("AllowAll");


app.Run();
