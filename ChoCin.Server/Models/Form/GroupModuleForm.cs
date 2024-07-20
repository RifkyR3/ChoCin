using System.ComponentModel.DataAnnotations;

namespace ChoCin.Server.Models.Form
{
    public class GroupModuleForm
    {
        [Required]
        public Guid GroupId { get; set; }
        [Required]
        public List<Guid> ModulesIds { get; set; }
    }
}
