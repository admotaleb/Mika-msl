using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Msl.Models
{
    public class Employee_Details
    {
        public int Id { get; set; }
        [DisplayName("Fathers Name")]
        public string Fathers_Name { get; set; }
        [DisplayName("Mothers Name")]
        public string Mothers_Name { get; set; }
        [DisplayName("Spouse Name")]
        public string Spouse_Name { get; set; }
        public string Desgnation { get; set; }
        [DisplayName("Dse Authorized")]
        public string DseAuthorized{ get; set; }
        [DisplayName("Joning Date")]
        public string Joning_Date { get; set; }
        [DisplayName("Blood Group")]
        public string Blood_Group { get; set; }
        public string NID { get; set; }
        [DisplayName("Personal Contact")]
        public string Contact_No_Personal { get; set; }
        [DisplayName("Emergency Contact")]
        public string EmergencyContact { get; set; }
        [DisplayName("Present Address")]
        public string Present_Address { get; set; }
        [DisplayName("Permanent Address")]
        public string Permanent_Address { get; set; }
        public string Pictures { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Gender { get; set; }
        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        [DisplayName("Date of birth")]
        public string Date_of_birth { get; set; }


        [NotMapped]
        [DisplayName("Picture")]
        public IFormFile ImageFile { get; set; }
    }
}
