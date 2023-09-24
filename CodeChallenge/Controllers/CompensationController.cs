using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using System;
using CodeChallenge.Models;
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

        [HttpGet("{employeeId}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeID(String employeeId)
        {
            _logger.LogDebug($"Received compensation get request by employee id '{employeeId}'");

            var employee = _employeeService.GetById(employeeId);

            if (employee == null)
                return NotFound("The employee with the given id does not exist."); // Might not want to have this verbose error in prod (info leak)

            // Get the compensation
            CompensationSingleDTO c = _compensationService.GetByEmployeeId(employeeId);
            return Ok(c);
        }

        // I've never done routes with the 'Name' param for the httpget attribute/annotation,
        // I've always specified the route and used query params, [FromQuery], and [FromBody] (some are implicit)
        [HttpPost] 
        public IActionResult CreateCompensation([FromBody] CompensationAddDTO compensationAddDTO)
        {
            // Check if providedEmployeeID exists. (arguably should be done in the service layer but would require more robust error/validation handling.)
            if(!_employeeService.Exists(compensationAddDTO.EmployeeID))
            {
                return BadRequest("Employee with the provided ID does not exist.");
            }

            // Check if a compensation already exists. (we don't want to be allowed to add multiple)
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
