using AutoMapper;
using MangaProjectEF.Dtos.Analysis.Response;
using MangaProjectEF.Interface;
using MangaProjectEF.UseCase.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProjectEF.UseCase.UseCases.Manga.Queries.GetAllQuery
{
    public class GetAllMangaHandler : IRequestHandler<GetAllMangaQuery, BaseResponse<IEnumerable<GetAllMangaResponseDto>>>
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;

        public GetAllMangaHandler(IMangaRepository mangaRepository, IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<GetAllMangaResponseDto>>> Handle(GetAllMangaQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IEnumerable<GetAllMangaResponseDto>>();


            try
            {
                var mangas = await _mangaRepository.ListManga();

                if(mangas is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<GetAllMangaResponseDto>>(mangas);
                    response.Message = "Success";
                }
            }catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
