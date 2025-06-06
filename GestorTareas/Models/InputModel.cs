using System.ComponentModel.DataAnnotations;

public class InputModel
{
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Display(Name = "Marcar como completada")]
    public bool Completada { get; set; } = false;
}