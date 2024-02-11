using MangaProject.Domain.Entities;

namespace MangaProject.Application.Interface.Interfaces
{
    public interface IMangaRepository
    {
        Task<IEnumerable<Manga>> ListManga();
        Task<Manga> MangaById(int mangaId);
        Task<bool> MangaRegister(Manga manga);
        Task<bool> MangaEdit(Manga manga);
        Task<bool> MangaRemove(int mangaId);
    }
}
