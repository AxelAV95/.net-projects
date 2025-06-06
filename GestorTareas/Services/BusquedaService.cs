using Microsoft.EntityFrameworkCore;
using GestorTareas.Data;
using GestorTareas.Models;

namespace GestorTareas.Services
{
    public interface IBusquedaService
    {
        Task<List<Tarea>> BuscarTareasAsync(string usuarioId, string termino);
        Task<List<Tarea>> FiltrarTareasAvanzadoAsync(string usuarioId, FiltroAvanzado filtro);
    }

    public class BusquedaService : IBusquedaService
    {
        private readonly AppDbContext _context;

        public BusquedaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tarea>> BuscarTareasAsync(string usuarioId, string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return new List<Tarea>();

            termino = termino.ToLower();

            return await _context.Tareas
                .Where(t => t.UsuarioId == usuarioId)
                .Where(t => 
                    t.Titulo.ToLower().Contains(termino) ||
                    (t.Descripcion != null && t.Descripcion.ToLower().Contains(termino)) ||
                    (t.Categoria != null && t.Categoria.ToLower().Contains(termino)) ||
                    (t.Etiquetas != null && t.Etiquetas.ToLower().Contains(termino)) ||
                    (t.Notas != null && t.Notas.ToLower().Contains(termino))
                )
                .OrderByDescending(t => t.FechaCreacion)
                .ToListAsync();
        }

        public async Task<List<Tarea>> FiltrarTareasAvanzadoAsync(string usuarioId, FiltroAvanzado filtro)
        {
            var query = _context.Tareas.Where(t => t.UsuarioId == usuarioId);

            // Filtro por estado
            if (filtro.Estado.HasValue)
            {
                query = query.Where(t => t.Completada == filtro.Estado.Value);
            }

            // Filtro por prioridad
            if (filtro.Prioridad.HasValue)
            {
                query = query.Where(t => t.Prioridad == filtro.Prioridad.Value);
            }

            // Filtro por categoría
            if (!string.IsNullOrWhiteSpace(filtro.Categoria))
            {
                query = query.Where(t => t.Categoria == filtro.Categoria);
            }

            // Filtro por fechas
            if (filtro.FechaDesde.HasValue)
            {
                query = query.Where(t => t.FechaCreacion.Date >= filtro.FechaDesde.Value.Date);
            }

            if (filtro.FechaHasta.HasValue)
            {
                query = query.Where(t => t.FechaCreacion.Date <= filtro.FechaHasta.Value.Date);
            }

            // Filtro por vencimiento
            if (filtro.SoloVencidas)
            {
                var hoy = DateTime.Now.Date;
                query = query.Where(t => t.FechaVencimiento.HasValue && 
                                        t.FechaVencimiento.Value.Date < hoy && 
                                        !t.Completada);
            }

            if (filtro.SoloVencenPronto)
            {
                var hoy = DateTime.Now.Date;
                var enTresDias = hoy.AddDays(3);
                query = query.Where(t => t.FechaVencimiento.HasValue && 
                                        t.FechaVencimiento.Value.Date >= hoy && 
                                        t.FechaVencimiento.Value.Date <= enTresDias && 
                                        !t.Completada);
            }

            // Ordenamiento
            query = filtro.OrdenPor switch
            {
                "fecha_desc" => query.OrderByDescending(t => t.FechaCreacion),
                "fecha_asc" => query.OrderBy(t => t.FechaCreacion),
                "prioridad_desc" => query.OrderByDescending(t => t.Prioridad),
                "prioridad_asc" => query.OrderBy(t => t.Prioridad),
                "vencimiento" => query.OrderBy(t => t.FechaVencimiento ?? DateTime.MaxValue),
                "titulo" => query.OrderBy(t => t.Titulo),
                _ => query.OrderByDescending(t => t.FechaCreacion)
            };

            return await query.ToListAsync();
        }
    }

    public class FiltroAvanzado
    {
        public bool? Estado { get; set; } // true = completadas, false = pendientes, null = todas
        public PrioridadTarea? Prioridad { get; set; }
        public string? Categoria { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public bool SoloVencidas { get; set; }
        public bool SoloVencenPronto { get; set; }
        public string OrdenPor { get; set; } = "fecha_desc";
    }
}