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
    public class DetalleModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetalleModel(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Tarea? Tarea { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                TempData["MensajeError"] = "ID de tarea no válido.";
                return RedirectToPage("./Index");
            }

            var usuarioId = _userManager.GetUserId(User);

            Tarea = await _context.Tareas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (Tarea == null)
            {
                TempData["MensajeError"] = "La tarea no existe o no tienes permisos para verla.";
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var usuarioId = _userManager.GetUserId(User);

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (tarea == null)
            {
                TempData["MensajeError"] = "La tarea no existe o no tienes permisos para eliminarla.";
                return RedirectToPage("./Index");
            }

            try
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();

                TempData["MensajeExito"] = $"La tarea '{tarea.Titulo}' ha sido eliminada exitosamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Error al eliminar la tarea. Inténtalo de nuevo.";
                return RedirectToPage("./Detalle", new { id = id });
            }
        }

        public async Task<IActionResult> OnPostCambiarEstadoAsync(int id)
        {
            var usuarioId = _userManager.GetUserId(User);

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (tarea != null)
            {
                tarea.Completada = !tarea.Completada;
                tarea.FechaModificacion = DateTime.Now;

                await _context.SaveChangesAsync();

                TempData["MensajeExito"] = tarea.Completada ?
                    "¡Tarea marcada como completada!" :
                    "Tarea marcada como pendiente.";
            }

            return RedirectToPage("./Detalle", new { id = id });
        }


    }
}
