using EventPlanning.DA.Models;
using System.Threading.Tasks;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Services.Abstract;
using AutoMapper;
using EventPlanning.DA.Repositories.Abstract;
using Microsoft.Extensions.Options;
using EventPlanning.Bl.Configuration;
using Hangfire;
using System;

namespace EventPlanning.Bl.Services
{
    public class ParticipationService : IParticipationService
    {
        private readonly IMapper _mapper;
        private readonly IParticipationRepository _participationRepository;
        private readonly ISmsService _smsService;
        private readonly IOptionsMonitor<AppConfiguration> _optionsMonitor;

        public ParticipationService(
            IMapper mapper, 
            IParticipationRepository participationRepository, 
            IOptionsMonitor<AppConfiguration> optionsMonitor, 
            ISmsService smsService)
        {
            _mapper = mapper;
            _participationRepository = participationRepository;
            _optionsMonitor = optionsMonitor;
            _smsService = smsService;
        }

        public async Task<int> AddParticipationAsync(ParticipationDTO participationDTO, int userId)
        {
            var participation = _mapper.Map<Participation>(participationDTO);
            participation.UserId = userId;
            await _participationRepository.AddParticipationAsync(participation);

            BackgroundJob.Enqueue(() => _smsService.SendCodeAsync(participation.VerifiedPhone, participation.VerifiedCode));
            BackgroundJob.Schedule(() => CheckAndRemoveAsync(participation.Id),
            TimeSpan.FromMinutes(int.Parse(_optionsMonitor.CurrentValue.PeriodVerifiedPhone)));

            return participation.Id;
        }

        public async Task CheckAndRemoveAsync(int participationId)
        {
            if (!(await _participationRepository.CheckVerified(participationId)))
                await _participationRepository.RemoveAsync(participationId);
        }

        public Task<bool> ExistsAsync(int participationId, int userId)
        {            
            return _participationRepository.ExistsAsync(participationId, userId);                   
        }

        public async Task<bool> VerifiAsync(ParticipationVerifiDTO participationVerifiDto)
        {
            var code = await _participationRepository.GetByAsync(participationVerifiDto.Id);
            if (code == participationVerifiDto.VerifiCode)
            {
                await _participationRepository.VerifiAsync(participationVerifiDto.Id);
                return true;
            }
            return false;            
        }
    }
}
