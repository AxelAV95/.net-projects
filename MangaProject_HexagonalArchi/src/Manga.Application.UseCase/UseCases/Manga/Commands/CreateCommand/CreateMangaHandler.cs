using AutoMapper;
using MangaProject.Application.UseCase.Commons.Bases;
using MediatR;
using Entity = MangaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaProject.Application.Interface.Interfaces;
using MangaProject.Utilities.HelperExtensions;

namespace MangaProject.Application.UseCase.UseCases.Manga.Commands.CreateCommand
{
    public class CreateMangaHandler : IRequestHandler<CreateMangaCommand, BaseResponse<bool>>
    {
        //private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMangaHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /*public CreateMangaHandler(IMangaRepository mangaRepository, IMapper mapper)
{
   _mangaRepository = mangaRepository;
   _mapper = mapper;
}*/

        public async Task<BaseResponse<bool>> Handle(CreateMangaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var manga = _mapper.Map<Entity.Manga>(request);
               var parameters = manga.GetPropertiesWithValues();
              //  var parameters = new { manga.Nombre, manga.Sinopsis, manga.Autor, manga.Tomo };
                response.Data = await _unitOfWork.Manga.ExecAsync("uspMangaRegister", parameters);

                if(response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registered successfully";
                }


            }catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
