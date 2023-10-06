namespace WebAPI.Models
{
    public class LoginResponse
    {
        public bool StatusOk { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public string IdUsuario { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // bearer token
        public DateTime Expiration { get; set; }
        public int ExpirationMinutes { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; }
    }
}
