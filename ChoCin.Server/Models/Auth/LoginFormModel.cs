using System.ComponentModel.DataAnnotations;

namespace ChoCin.Server.Models.Auth
{
    public class LoginFormModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "Username")]
        [Required]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool IsPersistent { get; set; }

        public string FullName { get; set; }
    }
}