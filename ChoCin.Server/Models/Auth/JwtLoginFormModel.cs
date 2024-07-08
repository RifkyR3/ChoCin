using System.ComponentModel.DataAnnotations;

namespace ChoCin.Server.Models.Auth
{
    public class JwtLoginFormModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
