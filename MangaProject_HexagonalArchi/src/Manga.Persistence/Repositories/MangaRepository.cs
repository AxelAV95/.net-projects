using Dapper;
using MangaProject.Application.Interface.Interfaces;
using MangaProject.Domain.Entities;
using MangaProject.Persistence.Context;

namespace MangaProject.Persistence.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly ApplicationDbContext _context;

        public MangaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Manga>> ListManga()
        {
            using var connection = _context.CreateConnection;
            var query = "uspMangaList";
            var manga = await connection.QueryAsync<Manga>(query, commandType: System.Data.CommandType.StoredProcedure);
            return manga;
        }

        public async Task<Manga> MangaById(int mangaId)
        {
            using var connection = _context.CreateConnection;
            var query = "uspMangaById";
            var parameters = new DynamicParameters();
            parameters.Add("MangaId", mangaId);

            var manga = await connection
                .QuerySingleOrDefaultAsync<Manga>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            return manga;
        }

        public async Task<bool> MangaEdit(Manga manga)
        {
            using var connection = _context.CreateConnection;
            var query = "uspMangaEdit";
            var parameters = new DynamicParameters();
            parameters.Add("MangaId", manga.MangaId);
            parameters.Add("Nombre", manga.Nombre);
            parameters.Add("Sinopsis", manga.Sinopsis);
            parameters.Add("Autor", manga.Autor);
            parameters.Add("Tomo", manga.Tomo);

            var recordsAffected = await connection
                .ExecuteAsync(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            return recordsAffected > 0;
        }

        public async Task<bool> MangaRegister(Manga manga)
        {
            using var connection = _context.CreateConnection;
            var query = "uspMangaRegister";
            var parameters = new DynamicParameters();
            parameters.Add("Nombre", manga.Nombre);
            parameters.Add("Sinopsis", manga.Sinopsis);
            parameters.Add("Autor", manga.Autor);
            parameters.Add("Tomo", manga.Tomo);

            var recordsAffected = await connection
                .ExecuteAsync(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            return recordsAffected > 0;


        }

        public async Task<bool> MangaRemove(int mangaId)
        {
            using var connection = _context.CreateConnection;
            var query = "uspMangaRemove";
            var parameters = new DynamicParameters();
            parameters.Add("MangaId", mangaId);
           

            var recordsAffected = await connection
                .ExecuteAsync(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            return recordsAffected > 0;
        }
    }
}
