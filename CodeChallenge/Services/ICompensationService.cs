using CodeChallenge.DTOs;
using System;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        CompensationSingleDTO GetByCompensationId(String compensationId);
        CompensationSingleDTO GetByEmployeeId(String employeeId);
        CompensationSingleDTO Create(CompensationAddDTO compensationDto);
    }
}
