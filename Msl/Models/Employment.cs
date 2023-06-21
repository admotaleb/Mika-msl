using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Msl.Models
{
    public class Employment
    {
        public int Id { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Company Business")]
        public string CompanyBusiness { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Responsibilities { get; set; }
        [DisplayName("Company Location")]
        public string CompanyLocation { get; set; }
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
