using EventPlanning.Bl.DTOs;
using FluentValidation;

namespace EventPlanning.Bl.Vlidators
{
    public class CaseCreateDTOValidator : AbstractValidator<CaseCreateDTO>
    {
        public CaseCreateDTOValidator()
        {
            RuleFor(w => w.UserId)
                .NotEmpty();
            RuleFor(w => w.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}