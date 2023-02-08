using System.ComponentModel.DataAnnotations;

namespace Msl.Models
{
    public class UserAssignVM
    {
        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
    }
}
