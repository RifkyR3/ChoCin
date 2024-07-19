using System.ComponentModel.DataAnnotations;

namespace ChoCin.Server.Models.Group
{
    public class AddUpdateGroup
    {
        [Required]
        public string GroupName { get; set; }
    }
}