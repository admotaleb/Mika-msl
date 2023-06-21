using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;

namespace Msl.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Tws { get; set; }
        //public string BranchName { get; set; }
        [DisplayName("Fast Name")]
        public string FastName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public List<Attendence> Attendences { get; set; }
        public int BranceSettingBranceId { get; set; }
        public BranceSetting BranceSetting { get; set; }
        public List<Employee_Details> employee_Details { get; set; }
        public Educational educationals  { get; set; }
        public List<Nominee> nominees  { get; set; }
        public List<Training> training  { get; set; }
        public List<Employment> Employments  { get; set; }
    }
}
