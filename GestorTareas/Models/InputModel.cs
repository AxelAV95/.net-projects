using System.ComponentModel.DataAnnotations;
using GestorTareas.Models;

public class InputModel
{
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "El título debe tener entre 3 y 200 caracteres")]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

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

    [Display(Name = "Tiempo estimado (minutos)")]
    [Range(0, 1440, ErrorMessage = "El tiempo debe estar entre 0 y 1440 minutos")]
    public int? TiempoEstimado { get; set; }

    [Display(Name = "Etiquetas (separadas por comas)")]
    [StringLength(200, ErrorMessage = "Las etiquetas no pueden exceder 200 caracteres")]
    public string? Etiquetas { get; set; }

    [Display(Name = "URL de referencia")]
    [StringLength(500, ErrorMessage = "La URL no puede exceder 500 caracteres")]
    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string? UrlReferencia { get; set; }

    [Display(Name = "Marcar como completada")]
    public bool Completada { get; set; } = false;
}