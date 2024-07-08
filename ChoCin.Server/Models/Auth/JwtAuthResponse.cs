using ChoCin.Server.Models.User;

namespace ChoCin.Server.Models.Auth
{
    public class JwtAuthResponse
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
