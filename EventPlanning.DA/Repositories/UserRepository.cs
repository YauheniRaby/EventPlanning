using EventPlanning.DA.Configuration;
using EventPlanning.DA.Models;
using EventPlanning.DA.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanning.DA.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _context;
        
        public UserRepository(RepositoryContext context)
        {
            _context = context;
        }

        public User GetByEmail(string login)
        {
            return _context.Users.Where(x => x.IsVerifiedEmail).FirstOrDefault(x => x.Login == login);
        }

        public User GetByIdAsync(int id)
        {
            return _context.Users.Where(x => x.IsVerifiedEmail).FirstOrDefault(x => x.Id == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Users.AnyAsync(x => x.Id == id && !x.IsRemove);
        }

        public Task<bool> ExistsAsync(string login)
        {
            return _context.Users.AnyAsync(x => x.Login == login);
        }

        public async Task<int> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public Task<bool> CheckVerifiedAsync(int id)
        {
            return _context.Users.AnyAsync(x => x.Id == id && x.IsVerifiedEmail);
        }

        public Task RemoveAsync(int id)
        {
            _context.Users.FirstOrDefault(x => x.Id == id).IsRemove = true;
            return _context.SaveChangesAsync();
        }

        public Task ConfirmEmailAsync(int id)
        {
            _context.Users.FirstOrDefault(x => x.Id == id).IsVerifiedEmail = true;
            return _context.SaveChangesAsync();
        }

        public Task<string> GetVerifiedCodeAsync(int id)
        {
            return _context.Users.Where(x => x.Id == id).Select(x => x.VerifiedCode).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(User user)
        {
            var currentDetail = _context.Users.Find(user.Id);
            _context.Entry(currentDetail).CurrentValues.SetValues(user);

            return _context.SaveChangesAsync();
        }

    }
}
