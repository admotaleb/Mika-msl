using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Msl.Models
{
    public class ActivitiesReport
    {
        public int Id { get; set; }
        public string Trader { get; set; }
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public double Turnover { get; set; }
        public int AverageClients { get; set; }
        public int? BoOpen { get; set; }
        public double Investment { get; set; }
        public double Withdraw { get; set; }
        public int? Visit { get; set; }
        public string ClientsNo { get; set; }
        public int ExpectedBoOpen { get; set; }        
        public int? ZoomMeeting { get; set; }
        public int? PhysicalVisit { get; set; }
        public int? Leave { get; set; }
        public int BranceSettingBranceId { get; set; }
        public BranceSetting BranceSetting { get; set; }



    }
}
