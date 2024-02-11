using MangaProject.Application.UseCase.UseCases.Manga.Commands.CreateCommand;
using MangaProject.Application.UseCase.UseCases.Manga.Commands.DeleteCommand;
using MangaProject.Application.UseCase.UseCases.Manga.Commands.UpdateCommand;
using MangaProject.Application.UseCase.UseCases.Manga.Queries.GetAllQuery;
using MangaProject.Application.UseCase.UseCases.Manga.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangaProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MangaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListManga()
        {
            var response = await _mediator.Send(new GetAllMangaQuery());
            return Ok(response);
        }

        [HttpGet("{mangaId:int}")]
        public async Task<IActionResult> MangaById(int mangaId)
        {
            var response = await _mediator.Send(new GetMangaByIdQuery() { MangaId = mangaId});
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterManga([FromBody] CreateMangaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("Edit/{mangaId}")]
        public async Task<IActionResult> EditManga(int mangaId, [FromBody] UpdateMangaCommand command)
        {
            // Aquí puedes usar el valor de mangaId en tu lógica de actualización
            command.MangaId = mangaId;

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("/{mangaId:int}")]
        public async Task<IActionResult> RemoveManga(int mangaId)
        {
            var response = await _mediator.Send(new DeleteMangaCommand() { MangaId = mangaId });
            return Ok(response);
        }


        /*[HttpPut("Edit")]
        public async Task<IActionResult> EditManga([FromBody] UpdateMangaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }]*/

    }
}
