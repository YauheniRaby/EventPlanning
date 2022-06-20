using EventPlanning.Bl.DTOs;
using FluentValidation;

namespace EventPlanning.Bl.Vlidators
{
    public class ConfirmEmailDTOValidator : AbstractValidator<ConfirmEmailDTO>
    {
        public ConfirmEmailDTOValidator()
        {
            RuleFor(w => w.UserId)
                .NotEmpty();
            RuleFor(w => w.VerifiedCode)
                .Length(36);
        }
    }
}