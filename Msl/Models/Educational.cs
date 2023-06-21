using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Msl.Models
{
    public class Educational
    {
        public int Id { get; set; }
        [DisplayName("Exam")]
        public string SscExam { get; set; }
        [DisplayName("Subject")]
        public string SscSubject { get; set; }
        [DisplayName("Institute")]
        public string SscInstitute_Name { get; set; }
        [DisplayName("Passing Year")]
        public string SscPassing_Year { get; set; }
        [DisplayName("Result")]
        public string SscResult { get; set; }
        [DisplayName("Exam")]
        public string HscExam { get; set; }
        [DisplayName("Subject")]
        public string HscSubject { get; set; }
        [DisplayName("Institute")]
        public string HscInstitute_Name { get; set; }
        [DisplayName("Passing Year")]
        public string HscPassing_Year { get; set; }
        [DisplayName("Result")]
        public string HscResult { get; set; }
        [DisplayName("Exam")]
        public string HonorsExam { get; set; }
        [DisplayName("Subject")]
        public string HonorsSubject { get; set; }
        [DisplayName("Institute")]
        public string HonorsInstitute_Name { get; set; }
        [DisplayName("Passing Year")]
        public string HonorsPassing_Year { get; set; }
        [DisplayName("Result")]
        public string HonorsResult { get; set; }
        [DisplayName("Exam")]
        public string MastersExam { get; set; }
        [DisplayName("Subject")]
        public string MastersSubject { get; set; }
        [DisplayName("Institute")]
        public string MastersInstitute_Name { get; set; }
        [DisplayName("Passing Year")]
        public string MastersPassing_Year { get; set; }
        [DisplayName("Result")]
        public string MastersResult { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
