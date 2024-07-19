using ChoCin.Server.Models.Group;
using System.ComponentModel.DataAnnotations;

namespace ChoCin.Server.Models.User
{
    public class AddUpdateUser
    {
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public List<GroupModel>? Groups { get; set; }
    }
}