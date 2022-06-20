using AutoMapper;
using EventPlanning.Bl.DTOs;
using EventPlanning.DA.Models;
using System;

namespace EventPlanning.AutoMap
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<CaseParamDTO, CaseParam>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Case, opt => opt.Ignore())
                .ForMember(dest => dest.CaseId, opt => opt.Ignore());
            CreateMap<CaseCreateDTO, Case>()
                .ForMember(dest => dest.CreatedDate, conf => conf.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.IsRemove, opt => opt.Ignore())
                .ForMember(dest => dest.Participations, opt => opt.Ignore());
            CreateMap<CaseShort, CaseShortDTO>();
            CreateMap<CaseView, CaseDTO>();
            CreateMap<CaseParamView, CaseParamDTO>();
        }
    }
}