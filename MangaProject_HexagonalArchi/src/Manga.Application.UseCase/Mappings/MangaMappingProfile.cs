using AutoMapper;
using MangaProject.Application.Dtos.Manga.Response;
using MangaProject.Application.UseCase.UseCases.Manga.Commands.CreateCommand;
using MangaProject.Application.UseCase.UseCases.Manga.Commands.DeleteCommand;
using MangaProject.Application.UseCase.UseCases.Manga.Commands.UpdateCommand;
using MangaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaProject.Application.UseCase.Mappings
{
    internal class MangaMappingProfile : Profile
    {
        public MangaMappingProfile()
        {
            CreateMap<Manga, GetAllMangaResponseDto>();
            CreateMap<Manga, GetMangaByIdResponseDto>().ReverseMap();
            CreateMap<CreateMangaCommand, Manga>();
            CreateMap<UpdateMangaCommand, Manga>();
            

        }



    }
}
