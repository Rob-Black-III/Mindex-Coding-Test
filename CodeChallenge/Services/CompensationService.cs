using CodeChallenge.DTOs;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ILogger<ICompensationService> _logger;
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeService _employeeService;
        public CompensationService(ILogger<ICompensationService> logger, ICompensationRepository compensationRepository, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
            _employeeService = employeeService;
        }
        public CompensationSingleDTO Create(CompensationAddDTO compensationDto)
        {
            // Map our request DTO to our Model Object
            // Normally I would use Mapster or Automapper but best not to overengineer for these simple cases.
            // In a large scale application I would generate mapping configs from src --> dest
            Compensation newC = new Compensation
            {
                EffectiveDate = compensationDto.EffectiveDate,
                Employee = _employeeService.GetById(compensationDto.EmployeeID),
                Salary = compensationDto.Salary
            };

            // Add the model to the db via the repository.
            Compensation addedCompensation = _compensationRepository.Add(newC);

            // Map our added model to our response DTO
            CompensationSingleDTO compSingleDTO = new CompensationSingleDTO
            {
                CompensationId = addedCompensation.CompensationId,
                EmployeeID = addedCompensation.Employee.EmployeeId,
                Salary = addedCompensation.Salary,
                EffectiveDate = addedCompensation.EffectiveDate
            };

            return compSingleDTO;
        }

        public CompensationSingleDTO GetByCompensationId(string compensationId)
        {
            Compensation compFromDb = _compensationRepository.GetByCompensationId(compensationId);

            // Map our added model to our response DTO
            CompensationSingleDTO compSingleDTO = new CompensationSingleDTO
            {
                CompensationId = compFromDb.CompensationId,
                EmployeeID = compFromDb.Employee.EmployeeId,
                Salary = compFromDb.Salary,
                EffectiveDate = compFromDb.EffectiveDate
            };

            return compSingleDTO;
        }

        public CompensationSingleDTO GetByEmployeeId(string employeeId)
        {
            Compensation compFromDb = _compensationRepository.GetByEmployeeId(employeeId);

            // Map our added model to our response DTO
            CompensationSingleDTO compSingleDTO = new CompensationSingleDTO
            {
                CompensationId = compFromDb.CompensationId,
                EmployeeID = compFromDb.Employee.EmployeeId,
                Salary = compFromDb.Salary,
                EffectiveDate = compFromDb.EffectiveDate
            };

            return compSingleDTO;
        }
    }
}
