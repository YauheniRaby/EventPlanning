using EventPlanning.Bl.DTOs;
using System.Threading.Tasks;

namespace EventPlanning.Bl.Services.Abstract
{
    public interface IParticipationService
    {
        Task<int> AddParticipationAsync(ParticipationDTO participationDTO, int userId);

        Task<bool> ExistsAsync(int participationId, int userId);

        Task<bool> VerifiAsync(ParticipationVerifiDTO participationVerifiDto);
    }
}
