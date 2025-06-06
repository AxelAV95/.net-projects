using GestorTareas.Data;
using GestorTareas.Models;
using GestorTareas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{[Authorize]
    [ValidateAntiForgeryToken]
    public class CrearModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITareaValidationService _validationService;
        private readonly ICategoriaService _categoriaService;
        private readonly ILogger<CrearModel> _logger;

        public CrearModel(
            AppDbContext context, 
            UserManager<IdentityUser> userManager,
            ITareaValidationService validationService,
            ICategoriaService categoriaService,
            ILogger<CrearModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _validationService = validationService;
            _categoriaService = categoriaService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public List<string> CategoriasUsuario { get; set; } = new();
        public List<string> CategoriasPopulares { get; set; } = new();

        
        public async Task<IActionResult> OnGetAsync()
        {
            await CargarDatosFormulario();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarDatosFormulario();
                return Page();
            }

            var usuarioId = _userManager.GetUserId(User);
            
            if (usuarioId == null)
            {
                _logger.LogWarning("Usuario no autenticado intentó crear tarea");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Validar fecha de vencimiento
            if (Input.FechaVencimiento.HasValue && Input.FechaVencimiento.Value.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(Input.FechaVencimiento), "La fecha de vencimiento no puede ser anterior a hoy");
                await CargarDatosFormulario();
                return Page();
            }

            var tarea = new Tarea
            {
                Titulo = Input.Titulo.Trim(),
                Descripcion = Input.Descripcion?.Trim(),
                FechaVencimiento = Input.FechaVencimiento,
                Prioridad = Input.Prioridad,
                Categoria = !string.IsNullOrWhiteSpace(Input.Categoria) ? 
                           _categoriaService.NormalizarCategoria(Input.Categoria.Trim()) : null,
                Notas = Input.Notas?.Trim(),
                TiempoEstimado = Input.TiempoEstimado,
                Etiquetas = Input.Etiquetas?.Trim(),
                UrlReferencia = Input.UrlReferencia?.Trim(),
                Completada = Input.Completada,
                FechaCreacion = DateTime.Now,
                UsuarioId = usuarioId
            };

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
                await CargarDatosFormulario();
                return Page();
            }
        }

        private async Task CargarDatosFormulario()
        {
            var usuarioId = _userManager.GetUserId(User);
            if (usuarioId != null)
            {
                CategoriasUsuario = await _categoriaService.ObtenerCategoriasUsuarioAsync(usuarioId);
            }
            CategoriasPopulares = _categoriaService.ObtenerCategoriasPopulares();
        }
    }
}
