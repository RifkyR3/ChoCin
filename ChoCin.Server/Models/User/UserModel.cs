namespace ChoCin.Server.Models.User
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string? UserFullName { get; set; }
    }
}
