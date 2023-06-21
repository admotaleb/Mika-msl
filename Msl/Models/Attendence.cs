using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;

namespace Msl.Models
{
    public class Attendence
    {
        
         public int Id { get;set; }
        [DisplayName("Check In")]
        public string CheckIn { get; set; }
        [DisplayName("Check In Reason")]
        public string CheckInReason { get; set; }
        public string Date { get; set; }
        [DisplayName("Check Out")]
        public string CheckOut { get; set; }
        [DisplayName("Check Out Reason")]
        public string CheckOutReason { get; set; }
        public string Present { get; set; }

        public string PrivateIp { get; set; }
        public string PublicIp { get; set; }
        public string HostName { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public string Longitude { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int BranceSettingBranceId { get; set; }
        public BranceSetting BranceSetting { get; set; }

    }
}
