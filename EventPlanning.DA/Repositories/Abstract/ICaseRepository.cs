using EventPlanning.DA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanning.DA.Repositories.Abstract
{
    public interface ICaseRepository
    {
        Task AddAsync(Case newCase);

        Task<List<CaseShort>> GetAllAsync();

        Task<bool> ExistsAsync(int id);

        Task<CaseView> GetByIdAsync(int id);

        Task<bool> CanAddedAsync(int id);
    }
}
