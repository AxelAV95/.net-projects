using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GestorTareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Completada")]
        public bool Completada { get; set; } = false;

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Display(Name = "Fecha de Modificación")]
        public DateTime? FechaModificacion { get; set; }

        // Relación con el usuario
        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        // Propiedad de navegación
        public IdentityUser? Usuario { get; set; }
    }
}