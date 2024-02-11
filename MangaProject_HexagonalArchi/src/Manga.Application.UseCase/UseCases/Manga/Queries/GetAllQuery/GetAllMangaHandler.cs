using AutoMapper;
using MangaProject.Application.Dtos.Manga.Response;
using MangaProject.Application.Interface.Interfaces;
using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProject.Application.UseCase.UseCases.Manga.Queries.GetAllQuery
{
    public class GetAllMangaHandler : IRequestHandler<GetAllMangaQuery, BaseResponse<IEnumerable<GetAllMangaResponseDto>>>
    {
        //private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMangaHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /*public GetAllMangaHandler(IMangaRepository mangaRepository, IMapper mapper)
{
   _mangaRepository = mangaRepository;
   _mapper = mapper;
}*/
        public async Task<BaseResponse<IEnumerable<GetAllMangaResponseDto>>> Handle(GetAllMangaQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<GetAllMangaResponseDto>>();

            try{
                var manga = await _unitOfWork.Manga.GetAllAsync("uspMangaList");
                if(manga is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<GetAllMangaResponseDto>>(manga);
                    response.Message = "Query done successfully";

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
