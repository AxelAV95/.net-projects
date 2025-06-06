using System.Text.RegularExpressions;
using GestorTareas.Models;

namespace GestorTareas.Services
{
    public interface ITareaValidationService
    {
        ValidationResult ValidarTarea(Tarea tarea);
        ValidationResult ValidarTitulo(string titulo);
        ValidationResult ValidarDescripcion(string? descripcion);
    }

    public class TareaValidationService : ITareaValidationService
    {
        private readonly List<string> _palabrasProhibidas = new()
        {
            // Añadir palabras que consideres inapropiadas
            "spam", "test123", "temporal"
        };

        public ValidationResult ValidarTarea(Tarea tarea)
        {
            var result = new ValidationResult();

            // Validar título
            var tituloResult = ValidarTitulo(tarea.Titulo);
            result.AddErrors(tituloResult.Errors);

            // Validar descripción
            var descripcionResult = ValidarDescripcion(tarea.Descripcion);
            result.AddErrors(descripcionResult.Errors);

            // Validaciones adicionales
            if (tarea.FechaCreacion > DateTime.Now.AddMinutes(5))
            {
                result.AddError("La fecha de creación no puede ser futura");
            }

            return result;
        }

        public ValidationResult ValidarTitulo(string titulo)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                result.AddError("El título es obligatorio");
                return result;
            }

            // Longitud
            if (titulo.Length < 3)
            {
                result.AddError("El título debe tener al menos 3 caracteres");
            }

            if (titulo.Length > 200)
            {
                result.AddError("El título no puede exceder 200 caracteres");
            }

            // Caracteres especiales excesivos
            var caracteresEspeciales = Regex.Matches(titulo, @"[!@#$%^&*(),.?\"":{}|<>]").Count;
            if (caracteresEspeciales > titulo.Length * 0.3)
            {
                result.AddError("El título contiene demasiados caracteres especiales");
            }

            // Palabras prohibidas
            foreach (var palabra in _palabrasProhibidas)
            {
                if (titulo.ToLower().Contains(palabra.ToLower()))
                {
                    result.AddError($"El título contiene contenido no permitido");
                    break;
                }
            }

            // Solo números
            if (Regex.IsMatch(titulo, @"^\d+$"))
            {
                result.AddError("El título no puede contener solo números");
            }

            return result;
        }

        public ValidationResult ValidarDescripcion(string? descripcion)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(descripcion))
            {
                return result; // La descripción es opcional
            }

            if (descripcion.Length > 1000)
            {
                result.AddError("La descripción no puede exceder 1000 caracteres");
            }

            // Verificar que no sea solo espacios o caracteres repetidos
            if (Regex.IsMatch(descripcion, @"^(.)\1{10,}$"))
            {
                result.AddError("La descripción no puede consistir en caracteres repetidos");
            }

            return result;
        }
    }

    public class ValidationResult
    {
        public List<string> Errors { get; } = new();
        public bool IsValid => !Errors.Any();

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }
    }
}