using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;

namespace MangaProject.Application.UseCase.UseCases.Manga.Commands.CreateCommand
{
    public class CreateMangaCommand : IRequest<BaseResponse<bool>>
    {
        public string? Nombre { get; set; }
        public string? Sinopsis { get; set; }
        public string? Autor { get; set; }
        public int Tomo { get; set; }
    }
}
