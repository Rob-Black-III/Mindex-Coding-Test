using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using System;
using CodeChallenge.DTOs;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<EmployeeController> logger, ICompensationService compService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogDebug($"[CompensationController][GetAll] Get All Compensations");
            return Ok(_compensationService.GetAll());
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetCompensationByEmployeeID(String employeeId)
        {
            _logger.LogDebug($"[CompensationController][GetCompensationByEmployeeID] Get Compensation for employee with id {employeeId}");

            // Validation check (could arguably be put in the service layer per style guidelines.)
            if (!_employeeService.Exists(employeeId))
            {
                return NotFound("The employee with the given id does not exist."); // Might not want to have this verbose error in prod (info leak)
            }

            // Get the compensation
            CompensationSingleDTO? c = _compensationService.GetByEmployeeId(employeeId);

            if(c is null)
            {
                return NotFound("The compensation with the provided employee id could not be found.");
            }
            else
            {
                return Ok(c);
            }
        }

        // I've never done routes with the 'Name' param for the httpget attribute/annotation,
        // I've always specified the route and used query params, [FromQuery], and [FromBody] (some are implicit)
        // I skipped validation for malformed payloads (model binding with dotnet core) as I think it is out of scope for what you are testing for. I can send you my Dotnet 7 boilerplate where i do so per request.
        [HttpPost] 
        public IActionResult CreateCompensation([FromBody] CompensationAddDTO compensationAddDTO)
        {
            _logger.LogDebug("[CompensationController][CreateCompensation] Creating compensation. ");

            // Check if providedEmployeeID exists. (arguably should be done in the service layer but would require more robust error/validation handling.)
            if (!_employeeService.Exists(compensationAddDTO.EmployeeID))
            {
                return NotFound("Employee with the provided ID does not exist.");
            }

            // Check if a compensation already exists.
            // (Assumption: we don't want to be allowed to add multiple, doesn't make conceptual sense unless we would be using effectivedate to 'overwrite old entries')
            // In that case we should just make a patch and keep 'create' for new entities only.
            if (_compensationService.ExistsForEmployeeWithId(compensationAddDTO.EmployeeID))
            {
                return BadRequest("Compensation with the provided employee ID already exists.");
            }

            // Create the compensation and return
            CompensationSingleDTO? responseDTO = _compensationService.Create(compensationAddDTO);
            if(responseDTO is null)
            {
                return StatusCode(500);
            }
            else
            {
                return Ok(responseDTO);
            }
        }
    }
}
