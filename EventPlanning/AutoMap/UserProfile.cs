using AutoMapper;
using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Helpers;
using EventPlanning.DA.Models;
using System;

namespace EventPlanning.AutoMap
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCredentialsDTO, User>()
                .ForMember(dest => dest.Salt, conf => conf.MapFrom(src => PasswordHelper.GetSalt()))
                .ForMember(dest => dest.HashPassword, conf => conf.MapFrom((src, dest) => PasswordHelper.GetHashPassword(src.Password, dest.Salt)))
                .ForMember(dest => dest.VerifiedCode, conf => conf.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerifiedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.IsRemove, opt => opt.Ignore())
                .ForMember(dest => dest.Phone, opt => opt.Ignore())
                .ForMember(dest => dest.Adress, opt => opt.Ignore())
                .ForMember(dest => dest.CasesOwner, opt => opt.Ignore())
                .ForMember(dest => dest.Participations, opt => opt.Ignore());
            CreateMap<UserInformationDTO, User>()
                .ForMember(dest => dest.Login, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerifiedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.HashPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.IsRemove, opt => opt.Ignore())
                .ForMember(dest => dest.CasesOwner, opt => opt.Ignore())
                .ForMember(dest => dest.VerifiedCode, opt => opt.Ignore())
                .ForMember(dest => dest.Participations, opt => opt.Ignore());
        }
    }
}