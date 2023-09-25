
using System;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        public int? CalculateDirectReports(string baseEmployeeId); 
        // I like to return nullable types for 'error cases',
        // to differentiate between normal execution (in the absence of a dedicated error handling middleware).
    }
}