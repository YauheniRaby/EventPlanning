using AutoMapper;
using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Services.Abstract;
using EventPlanning.DA.Models;
using EventPlanning.DA.Repositories.Abstract;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanning.Bl.Services
{
    public class CaseService : ICaseService
    {
        private readonly IMapper _mapper;
        private readonly ICaseRepository _caseRepository;

        public CaseService(IMapper mapper, ICaseRepository caseRepository)
        {
            _mapper = mapper;
            _caseRepository = caseRepository;
        }

        public Task AddAsync(CaseCreateDTO caseDTO, int userId)
        {
            var newCase = _mapper.Map<Case>(caseDTO);
            newCase.UserId = userId;
            return _caseRepository.AddAsync(newCase);
        }

        public async Task<List<CaseShortDTO>> GetAllAsync()
        {
            var result = _mapper.Map<List<CaseShortDTO>>(await _caseRepository.GetAllAsync());
            return result;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _caseRepository.ExistsAsync(id);
        }

        public Task<bool> CanAddedAsync(int id)
        {
            return _caseRepository.CanAddedAsync(id);
        }

        public async Task<CaseDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<CaseDTO>(await _caseRepository.GetByIdAsync(id));
        }  
    }
}
