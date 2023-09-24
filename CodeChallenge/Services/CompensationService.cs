using CodeChallenge.DTOs;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ILogger _logger;
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

            _logger.LogDebug($"[CompensationService][Create] Added compensation {compSingleDTO.CompensationId}."); // Would normally use a regex formatter in prod.

            return compSingleDTO;
        }

        // For debugging
        public List<Compensation> GetAll()
        {
            return _compensationRepository.GetAll();
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

            _logger.LogDebug($"[CompensationService][GetByCompensationId] Retrieved compensation {compSingleDTO.CompensationId}."); // Would normally use a regex formatter in prod.

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

            _logger.LogDebug($"[CompensationService][GetByEmployeeId] Retrieved compensation {compSingleDTO.CompensationId}."); // Would normally use a regex formatter in prod.

            return compSingleDTO;
        }
    }
}
