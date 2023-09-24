using System;
using CodeChallenge.DTOs;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingstructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;
        private readonly IEmployeeService _employeeService;

        // NOTE: I WOULD NEVER EVER ADD THIS MANY COMMENTS HAHA (THOUGH I DO LIKE DOCUMENTATION). I JUST WANT YOU ALL TO GET A FEEL FOR MY THOUGHTPROCESS

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService, IEmployeeService employeeService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
            _employeeService = employeeService; //Only here for BadRequest validation, other logic will be in service layer to prevent a bloated controller.
        }

        [HttpGet("{baseEmployeeId}", Name = "calculateDirectReportsForEmployee")]
        public IActionResult CalculateDirectReportsForEmployee([FromQuery] Guid baseEmployeeId)
        {
            // Dotnet will automatically perform a null check on model binding (Guid cannot be null), no need to guard 'baseEmployeeId'.
            // I chose to use GUID as an example even though the provided underlying structure is string, because GUID more applicable. I will use strings for task 2.

            // Short-circuit 'bad request' validation checking. Validation is also done downstream recursively in 'CalculateDirectReports'.
            // Checking in this layer allows us to determine the proper HTTP response code. Although it does couple our domain layer to our presentation layer in this case.
            Employee employee = _employeeService.GetById(baseEmployeeId.ToString());
            if(employee is null)
            {
                return BadRequest("The employee with the provided ID was not found.");
            }

            // Do the actual calculation
            int? numDirectReports = _reportingStructureService.CalculateDirectReports(baseEmployeeId);

            // Check if our calculation succeeded or if it exploded (why I used nullable types). I use null as error for non-null returns.
            // Normally I would built in a Result pattern discriminated union for determining the exact type of error. 
            if (numDirectReports.HasValue)
            {
                // We have everything we need for to wrap our response in our "dto" (ReportingStructure)
                // Would normally use a library like AutoMapper or Mapster for complex mappings.
                ReportingStructureSingleDTO report = new ReportingStructureSingleDTO
                {
                    NumberOfReports = numDirectReports.Value,
                    Employee = employee
                };
                return Ok(report);
            }

            // I like to have my 'else' cause be the error catch-all, and explicitly define all success clauses prior.
            // Alternatively could use BadRequest, though this was already validated above as being a valid ID provided. (We want to indicate something else unexpected happened downstream)
            return StatusCode(500);
            
        }

    }
}