﻿using FluentValidation;
using OutOfOffice.Application.LeaveRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Validators
{
    public class EditLeaveRequestValidator : AbstractValidator<EditLeaveRequestDto>
    {
        public EditLeaveRequestValidator()
        {
            RuleFor(e => e.AbsenceReasonId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");

            RuleFor(b => b.EndDate)
                .GreaterThanOrEqualTo(b => b.StartDate).WithMessage("End date must be equal or greater than start date.");
        }
    }
}
