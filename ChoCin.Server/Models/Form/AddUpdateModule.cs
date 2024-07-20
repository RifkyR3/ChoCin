using System.ComponentModel.DataAnnotations;

namespace ChoCin.Server.Models.Module
{
    public class AddUpdateModule
    {
        [Required]
        public string Name { get; set; }
        public string? Icon { get; set; }
        public string? Path { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public bool IsShowNav { get; set; }
        public Guid? SubModuleId { get; set; }
    }
}
