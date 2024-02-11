using MangaProjectEF.Interface;
using MangaProjectEF.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MangaProjectEF.Persistence.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly BdmangaContext _context;

        public MangaRepository(BdmangaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tbmanga>> ListManga()
        {
            return await _context.Tbmangas.ToListAsync();
        }
    }
}
