using System.Threading.Tasks;

namespace EventPlanning.Bl.Services.Abstract
{
    public interface ISmsService
    {
        Task SendCodeAsync(string phone, int verifiedCode);
    }
}
