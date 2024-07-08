namespace ChoCin.Server.Models.User
{
    public class AddUpdateUser
    {
        public string Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
