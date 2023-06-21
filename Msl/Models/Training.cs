using System.ComponentModel;

namespace Msl.Models
{
    public class Training
    {
        public int Id { get; set; }
        [DisplayName("Training Name")]
        public string TrainingName { get; set; }
        public string Institute { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Duration { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
