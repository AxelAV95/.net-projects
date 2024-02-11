using APIUsers7._0.Models;

namespace APIUsers7._0.Services.AuthService
{
    public interface IAuthInterface
    {
        Task<User> RegisterUser(UserDto request);
        Task<AuthResponseDto> Login(UserDto request);
    }
}
