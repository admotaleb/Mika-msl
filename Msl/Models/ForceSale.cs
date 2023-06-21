using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace Msl.Models
{
    public class ForceSale
    {
        public int Id { get; set; }
        [Display(Name ="A/C Code")]
        public string AC_Code { get; set; }
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Display(Name = "Market Value")]
        public string Market_Value { get; set; }
        [Display(Name = "Net Worth")]
        public string Net_Worth { get; set; }
        public string Balance { get; set; }
        public string Ratio { get; set; }
        public string TWS { get; set; }
        public string Trader { get; set; }
        public string Status { get; set; }
  
    }
}
