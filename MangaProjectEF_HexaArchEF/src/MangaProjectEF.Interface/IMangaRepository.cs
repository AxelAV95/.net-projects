using MangaProjectEF.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProjectEF.Interface
{
    public interface IMangaRepository
    {
        Task<IEnumerable<Tbmanga>> ListManga();
    }
}
