using System.ComponentModel.DataAnnotations;

namespace ChoCin.Server.Models.Form
{
    public class AddUpdateGroup
    {
        [Required]
        public string GroupName { get; set; }

        public List<Guid>? ModuleIds { get; set; }
    }
}