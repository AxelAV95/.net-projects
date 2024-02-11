namespace APIUsers7._0.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
    }
}
