using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_JWT.Services.AuthService
{
    public class AuthService : IAuthInterface
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }

        public async Task<User> RegisterUser(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User { 
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void CreatePasswordHash(string password, out string hashedPassword)
        {
            // Genera automáticamente un salt aleatorio y calcula el hash
            hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verifica si la contraseña proporcionada coincide con el hash almacenado
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<AuthResponseDto> Login(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if(user == null)
            {
                return new AuthResponseDto
                {
                    Message = "User not found"
                };
            }

            if(!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthResponseDto
                {
                    Message = "Wrong password"
                };
            }

            string tokenUser = token(user);

            return new AuthResponseDto
            {
                Success = true,
                Token = tokenUser

            };
        }

        private string CreateToken2(User user)
        {
            string key = "79gtQdi_C-t-qF7l_FvTg2XyDVDlTWT88A4S1iDznYb-YqEHav3ZBQm0K28JuAJ9d_gDzzQWu0GJocKUrg5kinyRK-T6sXMAMCRdatjwQcx4UysFOHR4UvWyDXsf-LthQKEChi_G2r4hAYTLyVWOek141lrt3IfQm52m8e3QFWM";
            //Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        private string token(User user)
        {
            string key = "79gtQdi_C-t-qF7l_FvTg2XyDVDlTWT88A4S1iDznYb-YqEHav3ZBQm0K28JuAJ9d_gDzzQWu0GJocKUrg5kinyRK-T6sXMAMCRdatjwQcx4UysFOHR4UvWyDXsf-LthQKEChi_G2r4hAYTLyVWOek141lrt3IfQm52m8e3QFWM";
            var tokenHandler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes(key);
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),

                    }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDes);

            return tokenHandler.WriteToken(token);



        }

        

        /*
         string password = "tuContraseña";
string hashedPassword;

PasswordHasher.CreatePasswordHash(password, out hashedPassword);

// Almacena "hashedPassword" en tu base de datos

// Verifica la contraseña
bool passwordMatches = PasswordHasher.VerifyPassword("otraContraseña", hashedPassword);
         */
    }
}
