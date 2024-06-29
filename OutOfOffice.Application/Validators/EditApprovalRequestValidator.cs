using FluentValidation;
using OutOfOffice.Application.ApprovalRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Validators
{
    public class EditApprovalRequestValidator : AbstractValidator<EditApprovalRequestDto>
    {
        public EditApprovalRequestValidator()
        {
            RuleFor(a => a.StatusId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");
        }
    }
}
