using MangaProject.Application.Interface.Interfaces;
using MangaProject.Domain.Entities;

namespace MangaProject.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Manga> Manga { get; }

        public UnitOfWork(IGenericRepository<Manga> manga)
        {
            Manga = manga;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
