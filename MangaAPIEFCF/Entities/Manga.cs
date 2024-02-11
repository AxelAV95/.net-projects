namespace MangaAPIEFCF.Entities
{
    public class Manga
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = string.Empty;
        public required string Autor { get; set; } = string.Empty;
    }
}
