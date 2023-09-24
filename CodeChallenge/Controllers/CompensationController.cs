using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<EmployeeController> logger, ICompensationService compService)
        {
            _logger = logger;
            _compensationService = compService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogDebug($"[CompensationController][GetAll] Get All Compensations");
            return Ok(_compensationService.GetAll());
        }
    }
}
