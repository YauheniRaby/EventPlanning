using EventPlanning.Bl.DTOs;
using FluentValidation;

namespace EventPlanning.Bl.Vlidators
{
    public class ParticipationVerifiDTOValidator : AbstractValidator<ParticipationVerifiDTO>
    {
        public ParticipationVerifiDTOValidator()
        {
            RuleFor(w => w.VerifiCode)
                .GreaterThanOrEqualTo(1000)
                .LessThanOrEqualTo(9999);
            RuleFor(w => w.Id)
                .NotEmpty();
        }
    }
}