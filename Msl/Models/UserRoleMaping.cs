using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Msl.Models
{
    public class UserRoleMaping
    {

        public string UserId { get; set; }

        public string RoleId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Role Name")]
        public string RoleName {get; set;}
    }
}
