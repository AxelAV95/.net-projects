using GestorTareas.Data;
using GestorTareas.Models;
using GestorTareas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    [Authorize]
    [ValidateAntiForgeryToken]
    public class CrearModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITareaValidationService _validationService;
        private readonly ILogger<CrearModel> _logger;

        public CrearModel(
            AppDbContext context,
            UserManager<IdentityUser> userManager,
            ITareaValidationService validationService,
            ILogger<CrearModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _validationService = validationService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();
        
         public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuarioId = _userManager.GetUserId(User);

            if (usuarioId == null)
            {
                _logger.LogWarning("Usuario no autenticado intentó crear tarea");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Verificar límite de tareas por usuario (opcional)
            var tareasExistentes = _context.Tareas.Count(t => t.UsuarioId == usuarioId);
            if (tareasExistentes >= 100) // Límite de 100 tareas por usuario
            {
                ModelState.AddModelError(string.Empty, "Has alcanzado el límite máximo de tareas (100). Elimina algunas tareas antes de crear nuevas.");
                return Page();
            }

            var tarea = new Tarea
            {
                Titulo = Input.Titulo.Trim(),
                Descripcion = Input.Descripcion?.Trim(),
                Completada = Input.Completada,
                FechaCreacion = DateTime.Now,
                UsuarioId = usuarioId
            };

            // Validaciones avanzadas
            var validationResult = _validationService.ValidarTarea(tarea);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return Page();
            }

            try
            {
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Tarea creada exitosamente por usuario {UserId}: {TareaId}", usuarioId, tarea.Id);

                TempData["MensajeExito"] = "¡Tarea creada exitosamente!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tarea para usuario {UserId}", usuarioId);
                ModelState.AddModelError(string.Empty, "Error al guardar la tarea. Inténtalo de nuevo.");
                return Page();
            }
        }
        // private readonly AppDbContext _context;
            // private readonly UserManager<IdentityUser> _userManager;
            // public CrearModel(AppDbContext context, UserManager<IdentityUser> userManager)
            // {
            //     _context = context;
            //     _userManager = userManager;
            // }

            // [BindProperty]
            // public InputModel Input { get; set; } = new();
            // public IActionResult OnGet()
            // {
            //     return Page();
            // }

            // public async Task<IActionResult> OnPostAsync()
            // {
            //     if (!ModelState.IsValid)
            //     {
            //         return Page();
            //     }

            //     var usuarioId = _userManager.GetUserId(User);

            //     if (usuarioId == null)
            //     {
            //         return RedirectToPage("/Account/Login", new { area = "Identity" });
            //     }

            //     var tarea = new Tarea
            //     {
            //         Titulo = Input.Titulo,
            //         Descripcion = Input.Descripcion,
            //         Completada = Input.Completada,
            //         FechaCreacion = DateTime.Now,
            //         UsuarioId = usuarioId
            //     };

            //     _context.Tareas.Add(tarea);

            //     try
            //     {
            //         await _context.SaveChangesAsync();
            //         TempData["MensajeExito"] = "¡Tarea creada exitosamente!";
            //         return RedirectToPage("./Index");
            //     }
            //     catch (Exception ex)
            //     {
            //         ModelState.AddModelError(string.Empty, "Error al guardar la tarea. Inténtalo de nuevo.");
            //         // En un entorno de producción, log the exception
            //         return Page();
            //     }
            // }


        }
}
