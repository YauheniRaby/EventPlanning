using EventPlanning.DA.Models;
using System.Threading.Tasks;

namespace EventPlanning.DA.Repositories.Abstract
{
    public interface IParticipationRepository
    {
        Task AddParticipationAsync(Participation participation);

        Task RemoveAsync(int participationId);

        public Task<bool> CheckVerified(int participationId);

        Task<bool> ExistsAsync(int participationId, int userId);

        Task VerifiAsync(int participationId);

        Task<int> GetByAsync(int participationId);        
    }
}
