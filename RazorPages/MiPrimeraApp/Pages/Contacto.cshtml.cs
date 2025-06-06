using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class ContactoModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }

        public bool Enviado { get; set; }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                Enviado = true;
            }
        }
    }
}
