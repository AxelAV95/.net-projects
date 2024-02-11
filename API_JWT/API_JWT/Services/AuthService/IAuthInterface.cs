namespace API_JWT.Services.AuthService
{
    public interface IAuthInterface
    {
        Task<User> RegisterUser(UserDto request);
        Task<AuthResponseDto> Login(UserDto request);
    }
}
