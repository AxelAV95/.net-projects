using AutoMapper;
using MangaProject.Application.Dtos.Manga.Response;
using MangaProject.Application.Interface.Interfaces;
using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;

namespace MangaProject.Application.UseCase.UseCases.Manga.Queries.GetByIdQuery
{
    public class GetMangaByIdHandler : IRequestHandler<GetMangaByIdQuery, BaseResponse<GetMangaByIdResponseDto>>
    {
        //private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetMangaByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /*public GetMangaByIdHandler(IMangaRepository mangaRepository, IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _mapper = mapper;
        }*/

        public async Task<BaseResponse<GetMangaByIdResponseDto>> Handle(GetMangaByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<GetMangaByIdResponseDto>();
            try
            {
                var manga = await _unitOfWork.Manga.GetByIdAsync("uspMangaById", new { request.MangaId } );
                if(manga is null)
                {
                    response.IsSuccess = false;
                    response.Message = "No register found";
                    return response;

                }

                response.IsSuccess = true;
                response.Data = _mapper.Map<GetMangaByIdResponseDto>(manga);
                response.Message = "Query done successfully";
            }catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
