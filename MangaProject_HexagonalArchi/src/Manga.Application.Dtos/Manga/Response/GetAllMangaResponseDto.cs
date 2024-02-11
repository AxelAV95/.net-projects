namespace MangaProject.Application.Dtos.Manga.Response
{
    public class GetAllMangaResponseDto
    {
        public int MangaId { get; set; }
        public string? Nombre { get; set; }
        public string? Sinopsis { get; set; }
        public string? Autor { get; set; }
        public int Tomo { get; set; }

    }
}
