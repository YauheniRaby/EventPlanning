using EventPlanning.Bl.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanning.Bl.Services.Abstract
{
    public interface ICaseService
    {
        Task AddAsync(CaseCreateDTO caseDTO);

        Task<List<CaseShortDTO>> GetAllAsync();

        Task<bool> ExistsAsync(int id);

        Task<CaseDTO> GetByIdAsync(int id);

        Task<bool> CanAddedAsync(int caseId);
    }
}
