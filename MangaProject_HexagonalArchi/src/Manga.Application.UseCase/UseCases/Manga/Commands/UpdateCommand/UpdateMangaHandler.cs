using AutoMapper;
using MangaProject.Application.Interface.Interfaces;
using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;
using Entity = MangaProject.Domain.Entities;

namespace MangaProject.Application.UseCase.UseCases.Manga.Commands.UpdateCommand
{
    public class UpdateMangaHandler : IRequestHandler<UpdateMangaCommand, BaseResponse<bool>>
    {
        //private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMangaHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /*public UpdateMangaHandler(IMangaRepository mangaRepository, IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _mapper = mapper;
        }*/

        public async Task<BaseResponse<bool>> Handle(UpdateMangaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var manga = _mapper.Map<Entity.Manga>(request);
                var parameters = new { manga.MangaId,manga.Nombre, manga.Sinopsis, manga.Autor, manga.Tomo };
                response.Data = await _unitOfWork.Manga.ExecAsync("uspMangaEdit", parameters);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Updated successfully";
                }


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
