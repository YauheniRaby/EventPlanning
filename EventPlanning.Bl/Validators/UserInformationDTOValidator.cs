using EventPlanning.Bl.DTOs;
using FluentValidation;

namespace EventPlanning.Bl.Vlidators
{
    public class UserInformationDTOValidator : AbstractValidator<UserInformationDTO>
    {
        public UserInformationDTOValidator()
        {
            RuleFor(w => w.Id)
                .NotNull();
            RuleFor(w => w.Name)
                .MaximumLength(25);
            RuleFor(w => w.Adress)
                .MaximumLength(100);
            RuleFor(w => w.Phone)
                .MaximumLength(15);
        }
    }
}