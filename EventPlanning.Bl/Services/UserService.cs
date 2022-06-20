using AutoMapper;
using EventPlanning.Bl.Configuration;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Services.Abstract;
using EventPlanning.DA.Models;
using EventPlanning.DA.Repositories.Abstract;
using Hangfire;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EventPlanning.Bl.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IOptionsMonitor<AppConfiguration> _optionsMonitor;

        public UserService (IUserRepository userRepository, IMapper mapper, IEmailService emailService, IOptionsMonitor<AppConfiguration> optionsMonitor)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
            _optionsMonitor = optionsMonitor;
        }

        public async Task Registration(UserCredentialsDTO userRegistration)
        {
            var user = _mapper.Map<User>(userRegistration);
            await _userRepository.AddAsync(user);
            
            BackgroundJob.Enqueue(() => _emailService.SendCodeAsync(user));
            BackgroundJob.Schedule(() => CheckVerifiedAndRemove(user.Id),
            TimeSpan.FromMinutes(int.Parse(_optionsMonitor.CurrentValue.PeriodConfirmEmail)));

            return;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _userRepository.ExistsAsync(id);
        }

        public Task<bool> ExistsAsync(string login)
        {
            return _userRepository.ExistsAsync(login);
        }

        public Task RemoveAsync(int id)
        {
            return _userRepository.RemoveAsync(id);
        }

        public async Task CheckVerifiedAndRemove(int id)
        {
            if(!(await _userRepository.CheckVerifiedAsync(id)))
                await RemoveAsync(id);
        }

        public async Task<bool> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO)
        {
            var actualCode = await _userRepository.GetVerifiedCodeAsync(confirmEmailDTO.UserId);
            if (confirmEmailDTO.VerifiedCode == actualCode)
            {
                await _userRepository.ConfirmEmailAsync(confirmEmailDTO.UserId);
                return true;
            }
            return false;
        }

        public Task AddMoreInformation(UserInformationDTO userInformationDto)
        {
            var cuurentUser = _userRepository.GetByIdAsync(userInformationDto.Id);
            var result = _mapper.Map(userInformationDto, cuurentUser);
            return _userRepository.UpdateAsync(result);
        }
    }
}
