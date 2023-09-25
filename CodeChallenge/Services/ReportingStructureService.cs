using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;

        public ReportingStructureService(ILogger<IReportingStructureService> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public int? CalculateDirectReports(string baseEmployeeId)
        {
            // Note: Given the nature of 'directReport' as opposed to 'indirectReport', etc,
            //      Assumption: The provided tree is a strict tree and not a graph (no cycles or loops)
            //          (direct reports to someone who direct reports to someone already reported haha),
            //          the case where the tree/graph? converges towards the leaves rather than diverges (not explicitly stated)
            //       I am aware that this code does not handle cycles and duplicate 'reporters', but the code would be out of scope compared to the provided example.
            //       Normally I would clarify with my boss for the 'edge-cases', but I'm going to make an assumption here given the nature of the code test.

            // Fetch employee with given 'baseEmployeeId' from the db via service.
            // Even though we already performed 'caller validation' I prefer 'callee' validation so I'm doing it again here. (keep responsibility in-house)
            if(!_employeeService.Exists(baseEmployeeId))
            {
                return null; // bubble up error.
            }

            // Recursively get the count
            return RecursivelyCalculateDirectReports(baseEmployeeId);
        }

        private int? RecursivelyCalculateDirectReports(string currentEmployeeId)
        {
            // Even though I said I prefer 'callee' validation, we already validated in this layer. No need to validate employee itself again.
            Employee currentEmployee = _employeeService.GetById(currentEmployeeId);
            if(currentEmployee.DirectReports != null && currentEmployee.DirectReports.Count != 0)
            {
                int total = currentEmployee.DirectReports.Count;
                foreach (Employee directReport in currentEmployee.DirectReports)
                {
                    // I would normally use more robust error handling than try/catch (global handler for error codes, etc). But it will do here.
                    try
                    {
                        // Get all current direct reporters + all direct reporters' reporters recursively.
                        // Slightly less time efficient than passing count as a parameter recursively but improves readability in my opinion.
                        total = total + (RecursivelyCalculateDirectReports(directReport.EmployeeId) ?? 0);
                    }
                    catch(Exception ex)
                    {
                        // Log the error
                        // Could catch each exception individually (case by case per business logic), but for demonstration and brevity this is sufficient.
                        _logger.LogError("[ReportingStructureService][RecursivelyCalculateDirectReports] An unexpected error occured.", ex.Message);

                        // Return null (to be converted to 500 in presentation layer)
                        return null;
                    }
                }
                return total;
                
            }
            _logger.LogDebug("[ReportingStructureService][RecursivelyCalculateDirectReports] Calculated all direct reports.");
            return 0;
        }

    }
}