using MangaAPIEFCF.Data;
using MangaAPIEFCF.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaAPIEFCF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly DataContext _context;

        public MangaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Manga>>> GetAllManga()
        {
            var mangas = await _context.Mangas.ToListAsync();
            return Ok(mangas);
        }


        [HttpGet("{id}")]
        //Optional: [Route(("{id}"))]
        public async Task<ActionResult<List<Manga>>> GetManga(int id)
        {
            var manga = await _context.Mangas.FindAsync(id);
            if(manga == null)
            {
                return NotFound("Manga not found");
            }
            return Ok(manga);
        }

        [HttpPost]
        //Optional: [Route(("{id}"))]
        public async Task<ActionResult<List<Manga>>> AddManga(Manga manga)
        {
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Manga>> UpdateManga(int id, Manga updatedManga)
        {
            var existingManga = await _context.Mangas.FindAsync(id);

            if (existingManga == null)
            {
                return NotFound(); // El manga no fue encontrado
            }

            // Actualizar propiedades del manga existente con los valores proporcionados
            existingManga.Nombre = updatedManga.Nombre;
            existingManga.Autor = updatedManga.Autor;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(existingManga);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Manga>> DeleteManga(int id)
        {
            var manga = await _context.Mangas.FindAsync(id);

            if (manga == null)
            {
                return NotFound(); // El manga no fue encontrado
            }

            // Remover el manga de la base de datos
            _context.Mangas.Remove(manga);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok(manga);
        }


    }
}
