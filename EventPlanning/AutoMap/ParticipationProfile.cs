using AutoMapper;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Helpers;
using EventPlanning.DA.Models;

namespace EventPlanning.AutoMap
{
    public class ParticipationProfile : Profile
    {
        public ParticipationProfile()
        {
            CreateMap<ParticipationDTO, Participation>()
                .ForMember(dest => dest.VerifiedCode, conf => conf.MapFrom(src => PasswordHelper.GetVerifiedSms()))
                .ForMember(dest => dest.IsVerified, conf => conf.MapFrom(src => false))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Case, opt => opt.Ignore());
        }
    }
}