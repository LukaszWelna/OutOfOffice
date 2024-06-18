using FluentValidation;
using OutOfOffice.Domain.Models;

namespace OutOfOffice.Application.Validators
{
    public class UserRoleDtoValidator : AbstractValidator<UserRoleDto>
    {
        public UserRoleDtoValidator()
        {
            RuleFor(ur => ur.SelectedUserId)
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(ur => ur.SelectedRoleId)
                .NotEmpty().WithMessage("This field is required.");
        }
    }
}