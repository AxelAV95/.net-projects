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

        // ===== NUEVAS PROPIEDADES =====

        [Display(Name = "Fecha de Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [Display(Name = "Prioridad")]
        public PrioridadTarea Prioridad { get; set; } = PrioridadTarea.Media;

        [StringLength(50, ErrorMessage = "La categoría no puede exceder 50 caracteres")]
        [Display(Name = "Categoría")]
        public string? Categoria { get; set; }

        [Display(Name = "Notas")]
        [StringLength(500, ErrorMessage = "Las notas no pueden exceder 500 caracteres")]
        public string? Notas { get; set; }

        [Display(Name = "Estimación de tiempo (minutos)")]
        [Range(0, 1440, ErrorMessage = "La estimación debe estar entre 0 y 1440 minutos (24 horas)")]
        public int? TiempoEstimado { get; set; }

        [Display(Name = "Etiquetas")]
        [StringLength(200, ErrorMessage = "Las etiquetas no pueden exceder 200 caracteres")]
        public string? Etiquetas { get; set; } // Separadas por comas

        [Display(Name = "URL de referencia")]
        [StringLength(500, ErrorMessage = "La URL no puede exceder 500 caracteres")]
        [Url(ErrorMessage = "Debe ser una URL válida")]
        public string? UrlReferencia { get; set; }

        // Relación con el usuario
        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        // Propiedad de navegación
        public IdentityUser? Usuario { get; set; }

        // ===== PROPIEDADES CALCULADAS =====

        [Display(Name = "Días hasta vencimiento")]
        public int? DiasHastaVencimiento => 
            FechaVencimiento.HasValue ? 
            (int)(FechaVencimiento.Value.Date - DateTime.Now.Date).TotalDays : 
            null;

        [Display(Name = "Está vencida")]
        public bool EstaVencida => 
            FechaVencimiento.HasValue && 
            FechaVencimiento.Value.Date < DateTime.Now.Date && 
            !Completada;

        [Display(Name = "Vence pronto")]
        public bool VencePronto => 
            DiasHastaVencimiento.HasValue && 
            DiasHastaVencimiento.Value <= 3 && 
            DiasHastaVencimiento.Value >= 0 && 
            !Completada;
    }

    public enum PrioridadTarea
    {
        [Display(Name = "Baja")]
        Baja = 1,
        
        [Display(Name = "Media")]
        Media = 2,
        
        [Display(Name = "Alta")]
        Alta = 3,
        
        [Display(Name = "Urgente")]
        Urgente = 4
    }
}