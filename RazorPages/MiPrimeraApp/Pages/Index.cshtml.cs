using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiPrimeraApp.Pages;

public class IndexModel : PageModel
{
    public string Mensaje { get; set; }
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Mensaje = "¡Hola desde el servidor! Probando Hot reload, si sirvió";
    }

    //     public async Task OnGetAsync()
    // {
    //     mensaje = await ObtenerMensajeAsync();
    // }

    //     public void OnPost()
    // {
    //     // Procesar formulario
    // }

    // public async Task<IActionResult> OnPostAsync()
    // {
    //     await GuardarDatosAsync();
    //     return RedirectToPage("Gracias");
    // }

    // <form method="post" asp-page-handler="Enviar">
    // public IActionResult OnPostEnviar()
    // {
    //     // Se ejecuta cuando el formulario tiene handler="Enviar"
    // }

}
