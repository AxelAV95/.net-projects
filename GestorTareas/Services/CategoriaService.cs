using GestorTareas.Data;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Services
{
    public interface ICategoriaService
    {
        Task<List<string>> ObtenerCategoriasUsuarioAsync(string usuarioId);
        List<string> ObtenerCategoriasPopulares();
        string NormalizarCategoria(string categoria);
    }

    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> ObtenerCategoriasUsuarioAsync(string usuarioId)
        {
            return await _context.Tareas
                .Where(t => t.UsuarioId == usuarioId && !string.IsNullOrEmpty(t.Categoria))
                .Select(t => t.Categoria!)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        public List<string> ObtenerCategoriasPopulares()
        {
            return new List<string>
            {
                "Trabajo",
                "Personal",
                "Hogar",
                "Salud",
                "Educación",
                "Finanzas",
                "Compras",
                "Proyectos",
                "Ejercicio",
                "Familia"
            };
        }

        public string NormalizarCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                return string.Empty;

            // Capitalizar primera letra
            return char.ToUpper(categoria[0]) + categoria.Substring(1).ToLower();
        }
    }
}