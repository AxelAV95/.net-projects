using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    [Authorize]
    public class CrearModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CrearModel(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var tarea = new Tarea
            {
                Titulo = Input.Titulo,
                Descripcion = Input.Descripcion,
                Completada = Input.Completada,
                FechaCreacion = DateTime.Now,
                UsuarioId = usuarioId
            };

            _context.Tareas.Add(tarea);
            
            try
            {
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "¡Tarea creada exitosamente!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar la tarea. Inténtalo de nuevo.");
                // En un entorno de producción, log the exception
                return Page();
            }
        }
    }
}
