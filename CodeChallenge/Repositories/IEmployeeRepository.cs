using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        List<Employee> GetAll(); // for debugging
        bool Exists(string id); // For short-circuit validation checks.
        Task SaveAsync();
    }
}