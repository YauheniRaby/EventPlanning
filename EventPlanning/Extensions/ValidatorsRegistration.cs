using EventPlanning.Bl.DTOs;
using EventPlanning.Bl.Vlidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Extensions
{
    public static class ValidatorsRegistration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CaseCreateDTO>, CaseCreateDTOValidator>();
            services.AddSingleton<IValidator<ConfirmEmailDTO>, ConfirmEmailDTOValidator>();
            services.AddSingleton<IValidator<ParticipationDTO>, ParticipationDTOValidator>();
            services.AddSingleton<IValidator<ParticipationVerifiDTO>, ParticipationVerifiDTOValidator>();
            services.AddSingleton<IValidator<UserCredentialsDTO>, UserCredentialsDTOValidator>();
            services.AddSingleton<IValidator<UserInformationDTO>, UserInformationDTOValidator>();
        }
    }
}
