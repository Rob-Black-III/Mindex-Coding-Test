using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _dbContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _dbContext.Compensation.Add(compensation);
            _dbContext.SaveChangesAsync().Wait(); // Normally I use async tasks, and not blocking operations, but im following the example in employee.
            return compensation;
        }

        public Compensation GetByCompensationId(string compensationID)
        {
            return _dbContext.Compensation.Find(compensationID); // Use find for primary keys
        }

        public Compensation GetByEmployeeId(string employeeId)
        {
            return _dbContext.Compensation.SingleOrDefault(c => c.Employee.EmployeeId == employeeId);
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
