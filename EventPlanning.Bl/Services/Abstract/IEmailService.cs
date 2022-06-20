using EventPlanning.DA.Models;
using System.Threading.Tasks;

namespace EventPlanning.Bl.Services.Abstract
{
    public interface IEmailService
    {
        Task SendCodeAsync(User user);
    }
}
