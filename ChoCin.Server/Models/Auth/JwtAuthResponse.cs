using ChoCin.Server.Models.Group;
using ChoCin.Server.Models.Module;

namespace ChoCin.Server.Models.Auth
{
    public class JwtAuthResponse
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public List<GroupModel>? Groups { get; set; }
        public List<ModuleModel>? Modules { get; set; }
    }
}