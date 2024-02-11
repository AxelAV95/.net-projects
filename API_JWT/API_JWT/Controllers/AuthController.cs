
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_JWT.Controllers
{

    

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authService;
        public AuthController(IAuthInterface authService)
        {
            this._authService = authService;
        }


        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(UserDto request)
        {

            var response = await _authService.RegisterUser(request);
            return Ok(response);

        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            var response = await _authService.Login(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);

                
        }

        [HttpGet, Authorize]
        public ActionResult<string> Test()
        {
            return Ok("Test");
        }
    }
}
