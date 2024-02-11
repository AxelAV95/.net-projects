using MangaProject.Application.Dtos.Manga.Response;
using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProject.Application.UseCase.UseCases.Manga.Queries.GetByIdQuery
{
   public class GetMangaByIdQuery : IRequest<BaseResponse<GetMangaByIdResponseDto>>
    {
        public int MangaId { get; set; }
    }
}
