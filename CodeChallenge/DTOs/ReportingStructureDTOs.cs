using CodeChallenge.Models;

namespace CodeChallenge.DTOs
{
    public record struct ReportingStructureSingleDTO
    {
        public int NumberOfReports { get; set; }  // Recursive sum of employee.directreports for self and children
        public Employee Employee { get; set; }    // The employee we are inquiring about (would normally use employeeID, but entire payload was defined in acceptance criteria.)
    }
}
