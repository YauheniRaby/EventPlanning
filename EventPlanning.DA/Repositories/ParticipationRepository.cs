using EventPlanning.DA.Configuration;
using EventPlanning.DA.Models;
using EventPlanning.DA.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanning.DA.Repositories
{
    public class ParticipationRepository : IParticipationRepository
    {
        private readonly RepositoryContext _context;

        public ParticipationRepository(RepositoryContext context)
        {
            _context = context;
        }

        public Task AddParticipationAsync(Participation participation)
        {
            _context.Participations.Add(participation);
            return _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int participationId)
        {
            var participations = await _context.Participations.FindAsync(participationId);
            _context.Participations.Remove(participations);
            await _context.SaveChangesAsync();
        }

        public Task<bool> CheckVerified(int participationId)
        {
            return _context.Participations.AnyAsync(x => x.Id == participationId && x.IsVerified);
        }

        public Task<bool> ExistsAsync(int participationId, int userId)
        {
            return _context.Participations.AnyAsync(x => x.Id == participationId && x.UserId == userId && !x.IsVerified);
        }

        public async Task VerifiAsync(int participationId)
        {
            var result = await _context.Participations.FindAsync(participationId);
            result.IsVerified = true;
            await _context.SaveChangesAsync();
        }

        public Task<int> GetByAsync(int participationId)
        {
            return _context.Participations.Where(x=> x.Id == participationId).Select(x=>x.VerifiedCode).FirstOrDefaultAsync();
        }
    }
}
