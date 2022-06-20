using EventPlanning.Bl.DTOs;
using FluentValidation;

namespace EventPlanning.Bl.Vlidators
{
    public class UserCredentialsDTOValidator : AbstractValidator<UserCredentialsDTO>
    {
        public UserCredentialsDTOValidator()
        {
            RuleFor(w => w.Login)
                .EmailAddress();
            RuleFor(w => w.Password)
                .MinimumLength(6);
        }
    }
}