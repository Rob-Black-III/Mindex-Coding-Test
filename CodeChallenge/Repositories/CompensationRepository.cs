using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _dbContext;
        private readonly ILogger _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public Compensation? Add(Compensation compensation)
        {
            try
            {
                compensation.CompensationId = Guid.NewGuid().ToString(); // Can specify autogeneration in EF settings. But will follow Employee convention
                _dbContext.Compensation.Add(compensation);
                _dbContext.SaveChangesAsync().Wait(); // Normally I use async tasks, and not blocking operations, but im following the example in employee.
                _logger.LogDebug($"[CompensationRepository][Add] Added compensation {compensation.CompensationId} to the database.");
                return compensation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[CompensationRepository][Add] Failed to add compensation to the database.", ex.Message);
                return null;
            }

        }

        public Compensation GetByCompensationId(string compensationID)
        {
            _logger.LogDebug($"[CompensationRepository][GetByCompensationId] Retrieving compensation with compensationID {compensationID} from the database.");
            // Assuming employee and compensation records are 1-1, and there cannot be multiple compensations for an employee.
            return _dbContext.Compensation.Include(c => c.Employee).SingleOrDefault(c => c.CompensationId == compensationID); // Include FK resolution for Employee
        }

        public Compensation GetByEmployeeId(string employeeId)
        {
            _logger.LogDebug($"[CompensationRepository][GetByEmployeeId] Retrieving compensation with employeeId {employeeId} from the database.");
            // Assuming employee and compensation records are 1-1, and there cannot be multiple compensations for an employee.
            return _dbContext.Compensation.Include(c => c.Employee).SingleOrDefault(c => c.Employee.EmployeeId == employeeId); // Include FK resolution for Employee
        }

        // For debugging
        public List<Compensation> GetAll()
        {
            return _dbContext.Compensation.Include(c => c.Employee).ToList(); ;
        }

        public Task SaveAsync()
        {
            _logger.LogDebug("[CompensationRepository][SaveAsync] Saving changes to DB via repository.");
            return _dbContext.SaveChangesAsync();
        }

        public bool Exists(string id)
        {
            return _dbContext.Compensation.Any(c => c.CompensationId == id);
        }

        public bool ExistsForEmployeeWithId(string employeeId)
        {
            return _dbContext.Compensation.Any(c => c.Employee.EmployeeId == employeeId);
        }
    }
}
