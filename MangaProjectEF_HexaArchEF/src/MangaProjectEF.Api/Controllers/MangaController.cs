using MangaProjectEF.UseCase.UseCases.Manga.Queries.GetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangaProjectEF.Api.Controllers
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
    }
}
