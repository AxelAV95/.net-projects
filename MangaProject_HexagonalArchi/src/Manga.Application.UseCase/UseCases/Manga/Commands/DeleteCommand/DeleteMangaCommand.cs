using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;

namespace MangaProject.Application.UseCase.UseCases.Manga.Commands.DeleteCommand
{
    public class DeleteMangaCommand : IRequest<BaseResponse<bool>>
    {
        public int MangaId;
    }
}
