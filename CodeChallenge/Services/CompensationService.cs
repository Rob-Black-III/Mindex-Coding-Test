using CodeChallenge.DTOs;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ILogger<ICompensationService> _logger;
        private readonly ICompensationRepository _compensationRepository;
        public CompensationService(ILogger<ICompensationService> logger, ICompensationRepository compensationRepository)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
        }
        public CompensationSingleDTO Create(CompensationAddDTO compensationDto)
        {
            throw new System.NotImplementedException();
        }

        public CompensationSingleDTO GetByCompensationId(string compensationId)
        {
            throw new System.NotImplementedException();
        }

        public CompensationSingleDTO GetByEmployeeId(string employeeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
