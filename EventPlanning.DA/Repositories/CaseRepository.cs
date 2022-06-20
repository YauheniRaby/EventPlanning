using EventPlanning.DA.Configuration;
using EventPlanning.DA.Models;
using EventPlanning.DA.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanning.DA.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly RepositoryContext _context;

        public CaseRepository(RepositoryContext context)
        {
            _context = context;
        }

        public Task AddAsync (Case newCase)
        {
            _context.Cases.Add(newCase);
            return _context.SaveChangesAsync();
        }

        public Task<List<CaseShort>> GetAllAsync()
        {
            return _context.Cases
                .Where(x => !x.IsRemove)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new CaseShort
                    {
                        Name = x.Name,
                        CreatedDate = x.CreatedDate,
                        Id = x.Id
                    })
                .ToListAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Cases.AnyAsync(x => x.Id == id && !x.IsRemove);
        }

        public Task<bool> CanAddedAsync(int id)
        {
            return _context.Cases
                .AnyAsync(x => 
                    x.Id == id 
                    && !x.IsRemove 
                    && (x.CountMembers == null || x.CountMembers>x.Participations.Count()));
        }

        public Task<CaseView> GetByIdAsync(int id)
        {
            return _context.Cases
                .Include(x => x.User)
                .Include(x => x.CaseParams)
                .Include(x => x.Participations)
                .Select(x => new CaseView
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CreatedDate = x.CreatedDate,
                        CountMembers = x.CountMembers,
                        CountMembersActual = x.Participations.Where(x => x.IsVerified).Count(),
                        UserId = x.UserId,
                        UserLogin = x.User.Login,
                        CaseParams = x.CaseParams.Select(y => new CaseParamView { Name = y.Name, Value = y.Value })
                    })                
                .FirstAsync(x=>x.Id == id);
        }        
    }
}
