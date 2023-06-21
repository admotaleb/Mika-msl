using System.ComponentModel;

namespace Msl.Models
{
    public class Nominee
    {
        public int Id { get; set; }
        [DisplayName("Nomenne Name")]
        public string NomineeName { get; set; }
        [DisplayName("Legal Guardian Name")]
        public string LegalGuardianName { get; set; }
        public string Address { get; set; }
        public string NID { get; set; }
        public string Contact { get; set; }
        public string Relation { get; set; }
        [DisplayName("Date Of Birth")]
        public string DateOfBirth { get; set; }
        public string Entitlement { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
