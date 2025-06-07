using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GestorTareas.Data;
using GestorTareas.Models;
using GestorTareas.Services;
using Xunit;
using FluentAssertions;

namespace GestorTareas.Tests.Integration
{
    public class TareasSimpleIntegrationTests
    {
        [Fact]
        public void DbContext_DebeCrearseCorrectamente()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            var serviceProvider = services.BuildServiceProvider();

            // Act
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Assert
            context.Should().NotBeNull();
            context.Database.EnsureCreated().Should().BeTrue();
        }

        [Fact]
        public void TareaValidationService_DebeInstanciarse()
        {
            // Arrange & Act
            var service = new TareaValidationService();

            // Assert
            service.Should().NotBeNull();
        }

        [Fact]
        public void Tarea_DebeGuardarseEnBaseDatos()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb2"));

            var serviceProvider = services.BuildServiceProvider();

            // Act
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();

            var tarea = new Tarea
            {
                Titulo = "Test Integration",
                UsuarioId = "user123",
                FechaCreacion = DateTime.Now
            };

            context.Tareas.Add(tarea);
            context.SaveChanges();

            // Assert
            var tareaGuardada = context.Tareas.FirstOrDefault();
            tareaGuardada.Should().NotBeNull();
            tareaGuardada!.Titulo.Should().Be("Test Integration");
        }
    }
}