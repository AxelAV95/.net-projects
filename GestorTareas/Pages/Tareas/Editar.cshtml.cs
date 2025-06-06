using System.ComponentModel.DataAnnotations;
using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Authorize]
    public class EditarModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditarModel(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public Tarea? Tarea { get; set; }

        public class InputModel
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
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioId = _userManager.GetUserId(User);

            Tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (Tarea == null)
            {
                TempData["MensajeError"] = "La tarea no existe o no tienes permisos para editarla.";
                return RedirectToPage("./Index");
            }

            // Cargar los datos existentes en el formulario
            Input = new InputModel
            {
                Id = Tarea.Id,
                Titulo = Tarea.Titulo,
                Descripcion = Tarea.Descripcion,
                Completada = Tarea.Completada
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar la tarea para mostrar información en la vista
                var usuarioId = _userManager.GetUserId(User);
                Tarea = await _context.Tareas
                    .FirstOrDefaultAsync(t => t.Id == Input.Id && t.UsuarioId == usuarioId);

                return Page();
            }

            var usuarioIdPost = _userManager.GetUserId(User);

            var tareaActualizar = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == Input.Id && t.UsuarioId == usuarioIdPost);

            if (tareaActualizar == null)
            {
                TempData["MensajeError"] = "La tarea no existe o no tienes permisos para editarla.";
                return RedirectToPage("./Index");
            }

            // Actualizar solo los campos modificables
            tareaActualizar.Titulo = Input.Titulo;
            tareaActualizar.Descripcion = Input.Descripcion;
            tareaActualizar.Completada = Input.Completada;
            tareaActualizar.FechaModificacion = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "¡Tarea actualizada exitosamente!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar la tarea. Inténtalo de nuevo.");

                // Recargar la tarea para mostrar en la vista
                Tarea = tareaActualizar;
                return Page();
            }
        }
    }
    
}
