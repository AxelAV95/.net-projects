namespace MangaProject.Domain.Entities
{
    public class Manga
    {
        public int MangaId { get; set; }
        public string? Nombre { get; set; }
        public string? Sinopsis { get; set; }
        public string? Autor { get; set; }
        public int Tomo { get; set; }
    }
}
