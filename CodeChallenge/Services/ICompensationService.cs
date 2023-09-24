using CodeChallenge.DTOs;
using CodeChallenge.Models;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        CompensationSingleDTO GetByCompensationId(String compensationId);
        CompensationSingleDTO GetByEmployeeId(String employeeId);
        CompensationSingleDTO Create(CompensationAddDTO compensationDto);

        List<Compensation> GetAll(); // For debugging and testing
    }
}
