using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProject.Application.Dtos.Manga.Response
{
    public class GetMangaByIdResponseDto
    {
        public int MangaId { get; set; }
        public string? Nombre { get; set; }
        public string? Sinopsis { get; set; }
        public string? Autor { get; set; }
        public int Tomo { get; set; }
    }
}
