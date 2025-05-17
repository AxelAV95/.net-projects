using Blog.Domain.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Options;

public class TokenServiceTests
{
    [Fact]
    public void GenerateToken_ReturnsValidToken()
    {
        // Arrange
        var settings = Options.Create(new JwtSettings
        {
            SecretKey = "SuperSecretaClaveDe32Caracteres123456",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpiresInMinutes = 60
        });

        var service = new TokenService(settings);
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "testuser",
            Role = "User",
            Email = "testing@gmail.com"
        };

        // Act
        var token = service.GenerateToken(user);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }
}