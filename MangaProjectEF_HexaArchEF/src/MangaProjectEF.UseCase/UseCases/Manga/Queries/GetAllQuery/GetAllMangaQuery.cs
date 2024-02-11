using MangaProjectEF.Dtos.Analysis.Response;
using MangaProjectEF.UseCase.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProjectEF.UseCase.UseCases.Manga.Queries.GetAllQuery
{
    public class GetAllMangaQuery : IRequest<BaseResponse<IEnumerable<GetAllMangaResponseDto>>>
    {

    }
}
