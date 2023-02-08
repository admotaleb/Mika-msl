using Microsoft.AspNetCore.Identity;

namespace Msl.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Tws { get; set; }
        public string BranchName { get; set; }
    }
}
