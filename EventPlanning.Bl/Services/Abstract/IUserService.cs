using EventPlanning.Bl.DTOs;
using System.Threading.Tasks;

namespace EventPlanning.Bl.Services.Abstract
{
    public interface IUserService
    {
        Task Registration(UserCredentialsDTO userRegistration);

        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(string login);

        Task<bool> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO);

        Task AddMoreInformation(UserInformationDTO userInformationDto);        
    }
}
