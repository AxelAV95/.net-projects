using FluentAssertions;
using GestorTareas.Models;
using GestorTareas.Services;
using Xunit;

namespace GestorTareas.Tests.Services
{
    public class TareaValidationServiceTests
    {
        private readonly TareaValidationService _service;

        public TareaValidationServiceTests()
        {
            _service = new TareaValidationService();
        }

        [Fact]
        public void ValidarTitulo_ConTituloValido_DebeRetornarExito()
        {
            // Arrange
            var titulo = "Tarea válida de prueba";

            // Act
            var resultado = _service.ValidarTitulo(titulo);

            // Assert
            resultado.IsValid.Should().BeTrue();
            resultado.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ValidarTitulo_ConTituloVacio_DebeRetornarError(string titulo)
        {
            // Act
            var resultado = _service.ValidarTitulo(titulo);

            // Assert
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain("El título es obligatorio");
        }

        [Fact]
        public void ValidarTitulo_ConTituloMuyCorto_DebeRetornarError()
        {
            // Arrange
            var titulo = "Ab";

            // Act
            var resultado = _service.ValidarTitulo(titulo);

            // Assert
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain("El título debe tener al menos 3 caracteres");
        }

        [Fact]
        public void ValidarTitulo_ConTituloMuyLargo_DebeRetornarError()
        {
            // Arrange
            var titulo = new string('a', 201);

            // Act
            var resultado = _service.ValidarTitulo(titulo);

            // Assert
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain("El título no puede exceder 200 caracteres");
        }

        [Fact]
        public void ValidarTitulo_ConSoloNumeros_DebeRetornarError()
        {
            // Arrange
            var titulo = "123456";

            // Act
            var resultado = _service.ValidarTitulo(titulo);

            // Assert
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain("El título no puede contener solo números");
        }

        [Fact]
        public void ValidarDescripcion_ConDescripcionVacia_DebeRetornarExito()
        {
            // Act
            var resultado = _service.ValidarDescripcion(null);

            // Assert
            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
        public void ValidarDescripcion_ConDescripcionMuyLarga_DebeRetornarError()
        {
            // Arrange
            var descripcion = new string('a', 1001);

            // Act
            var resultado = _service.ValidarDescripcion(descripcion);

            // Assert
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain("La descripción no puede exceder 1000 caracteres");
        }

        [Fact]
        public void ValidarTarea_ConTareaCompleta_DebeValidarTodosLosCampos()
        {
            // Arrange
            var tarea = new Tarea
            {
                Titulo = "Tarea de prueba",
                Descripcion = "Descripción válida",
                FechaCreacion = DateTime.Now,
                UsuarioId = "user123"
            };

            // Act
            var resultado = _service.ValidarTarea(tarea);

            // Assert
            resultado.IsValid.Should().BeTrue();
        }
    }
}