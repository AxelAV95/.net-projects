using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyApp.Namespace
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(AppDbContext context, UserManager<IdentityUser> userManager, ILogger<IndexModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public IList<Tarea> Tareas { get; set; } = new List<Tarea>();
        [BindProperty(SupportsGet = true)]
        public string? Filtro { get; set; }

        public int TotalTareas { get; set; }
        public int TareasCompletadas { get; set; }
        public int TareasPendientes { get; set; }
        public async Task OnGetAsync()
        {
            var usuarioId = _userManager.GetUserId(User);

            if (usuarioId != null)
            {
                // Consulta base: todas las tareas del usuario
                var query = _context.Tareas
                    .Where(t => t.UsuarioId == usuarioId);

                // Aplicar filtro según la selección
                query = Filtro switch
                {
                    "completadas" => query.Where(t => t.Completada),
                    "pendientes" => query.Where(t => !t.Completada),
                    _ => query // "todas" o null
                };

                // Obtener las tareas ordenadas por fecha de creación (más recientes primero)
                Tareas = await query
                    .OrderByDescending(t => t.FechaCreacion)
                    .ToListAsync();

                // Calcular estadísticas
                var todasLasTareas = await _context.Tareas
                    .Where(t => t.UsuarioId == usuarioId)
                    .ToListAsync();

                TotalTareas = todasLasTareas.Count;
                TareasCompletadas = todasLasTareas.Count(t => t.Completada);
                TareasPendientes = todasLasTareas.Count(t => !t.Completada);
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

                _logger.LogInformation("Tarea {TareaId} cambió estado a {Estado} para usuario {UsuarioId}",
                    id, tarea.Completada ? "Completada" : "Pendiente", usuarioId);
            }

            return new JsonResult(new { success = true, completada = tarea?.Completada ?? false });
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var usuarioId = _userManager.GetUserId(User);

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);

            if (tarea == null)
            {
                TempData["MensajeError"] = "La tarea no existe o no tienes permisos para eliminarla.";
                return RedirectToPage();
            }

            try
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();

                _logger?.LogInformation("Tarea eliminada: {TareaId} por usuario {UsuarioId}", id, usuarioId);

                TempData["MensajeExito"] = $"¡Tarea '{tarea.Titulo}' eliminada exitosamente!";
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error al eliminar tarea {TareaId} para usuario {UsuarioId}", id, usuarioId);
                TempData["MensajeError"] = "Error al eliminar la tarea. Inténtalo de nuevo.";
            }

            return RedirectToPage();
        }
    }
}
