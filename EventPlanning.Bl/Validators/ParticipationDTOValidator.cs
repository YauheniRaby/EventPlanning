using EventPlanning.Bl.DTOs;
using FluentValidation;

namespace EventPlanning.Bl.Vlidators
{
    public class ParticipationDTOValidator : AbstractValidator<ParticipationDTO>
    {
        public ParticipationDTOValidator()
        {
            RuleFor(w => w.CaseId)
                .NotEmpty();                
            RuleFor(w => w.VerifiedPhone)
                .NotEmpty()
                .MaximumLength(15);            
        }
    }
}