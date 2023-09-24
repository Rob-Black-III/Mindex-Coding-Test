using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        // Provides the DAL to our service layer to uncouple our domain models from the other la
        Compensation GetByCompensationId(String compensationId);
        Compensation GetByEmployeeId(String employeeId);
        Compensation Add(Compensation compensation);
        List<Compensation> GetAll(); // For debugging and testing
        Task SaveAsync();
    }
}
