using Microsoft.AspNetCore.Mvc;


namespace MangaAPIEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly BdmangaContext _context;

        public MangaController(BdmangaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManga()
        {
            return Ok(await _context.Tbmangas.ToListAsync());
        }
    }
}
