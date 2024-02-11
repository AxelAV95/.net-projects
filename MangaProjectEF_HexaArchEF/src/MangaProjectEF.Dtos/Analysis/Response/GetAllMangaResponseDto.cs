using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProjectEF.Dtos.Analysis.Response
{
    public class GetAllMangaResponseDto
    {
        public int Mangaid { get; set; }

        public string Manganombre { get; set; } = null!;

        public int Mangatomo { get; set; }

        public DateTime Mangaregistro { get; set; }
    }
}
