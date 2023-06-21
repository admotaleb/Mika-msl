using System.ComponentModel;

namespace Msl.Models
{
    public class TimeSetting
    {
        public int Id { get; set; }
        [DisplayName("In Time")]
        public string InTime { get; set; }
        [DisplayName("Out Time")]
        public string OutTime { get; set; }
    }
}
