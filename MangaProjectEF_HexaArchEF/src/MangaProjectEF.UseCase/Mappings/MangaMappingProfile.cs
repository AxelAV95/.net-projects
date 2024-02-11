using AutoMapper;
using MangaProjectEF.Dtos.Analysis.Response;
using MangaProjectEF.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProjectEF.UseCase.Mappings
{
    public class MangaMappingProfile : Profile
    {
        public MangaMappingProfile()
        {
            CreateMap<Tbmanga, GetAllMangaResponseDto>();
        }
    }
}
