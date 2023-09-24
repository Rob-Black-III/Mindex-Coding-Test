
using System;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        public int? CalculateDirectReports(Guid baseEmployeeId); // I like to return nullable types for 'error cases', to differentiate between normal execution.
    }
}