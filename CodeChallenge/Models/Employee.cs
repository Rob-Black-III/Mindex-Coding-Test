using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Employee
    {
        public String EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public String Department { get; set; }
        public List<Employee> DirectReports { get; set; } // TODO RELATED TO BUG

        // Would normally configure both sides of the relationship, but this is not in the requirements.
        //[ForeignKey("CompensationID")]
        //public string CompensationID { get; set; }
        //public Compensation Compensation { get; set; }
    }
}
