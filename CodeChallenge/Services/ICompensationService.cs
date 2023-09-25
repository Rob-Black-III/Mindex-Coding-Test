using CodeChallenge.DTOs;
using CodeChallenge.Models;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        CompensationSingleDTO GetByCompensationId(String compensationId);
        CompensationSingleDTO? GetByEmployeeId(String employeeId);
        CompensationSingleDTO? Create(CompensationAddDTO compensationDto); // Using null as error handling just to show an example
        bool Exists(string id); // User for short-circuit checks to throw BadRequest.
        bool ExistsForEmployeeWithId(string employeeId); // User for short-circuit checks to throw BadRequest.
        List<Compensation> GetAll();
    }
}
