using System.ComponentModel.DataAnnotations;

namespace Msl.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
