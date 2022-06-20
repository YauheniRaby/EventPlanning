using EventPlanning.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.DA.Repositories.Abstract
{
    public interface IUserRepository
    {
        User GetByIdAsync(int id);

        User GetByEmail(string login);

        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(string login);

        Task<int> AddAsync(User user);

        Task<bool> CheckVerifiedAsync(int id);

        Task RemoveAsync(int id);

        Task ConfirmEmailAsync(int id);

        Task<string> GetVerifiedCodeAsync(int id);

        Task UpdateAsync(User user);
    }
}
