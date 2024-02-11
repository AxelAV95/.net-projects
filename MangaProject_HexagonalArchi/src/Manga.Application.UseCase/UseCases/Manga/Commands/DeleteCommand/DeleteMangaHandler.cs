using MangaProject.Application.Interface.Interfaces;
using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;

namespace MangaProject.Application.UseCase.UseCases.Manga.Commands.DeleteCommand
{
    public class DeleteMangaHandler : IRequestHandler<DeleteMangaCommand, BaseResponse<bool>>
    {
       // private readonly IMangaRepository _mangaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMangaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /* public DeleteMangaHandler(IMangaRepository mangaRepository)
{
    _mangaRepository = mangaRepository;
}*/

        public async Task<BaseResponse<bool>> Handle(DeleteMangaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                response.Data = await _unitOfWork.Manga.ExecAsync("uspMangaRemove", new { request.MangaId } );
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Deleted successfully";
                }

            }catch(Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;

        }
    }
}
