using System.Collections.Generic;
using System.ComponentModel;

namespace Msl.Models
{
    public class Department
    {
        public int Id {get; set;}
        [DisplayName("Department Name")]
        public string Name { get; set; }
        public List<Employee_Details> Employee_Details { get; set; }
    }
}
