using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Msl.Models
{
    public class BranceSetting
    {
        [Key]
        public int BranceId { get; set; }
        [DisplayName("Brance Name")]
        public string Name { get; set; }
        [DisplayName("Ip Address")]
        public string IpAddress { get; set; }
        public List<Attendence> Attendences { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public List<ActivitiesReport> ActivitiesReports { get; set; }
    }
}
