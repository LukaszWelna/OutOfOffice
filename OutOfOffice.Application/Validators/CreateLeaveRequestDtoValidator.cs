﻿using FluentValidation;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.LeaveRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Validators
{
    public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
    {
        public CreateLeaveRequestDtoValidator()
        {
            RuleFor(l => l.AbsenceReasonId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");

            RuleFor(l => l.EndDate)
                .GreaterThanOrEqualTo(l => l.StartDate).WithMessage("End date must be equal or greater than start date.");
        }
    }
}
