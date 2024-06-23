using FluentValidation;
using OutOfOffice.Application.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Validators
{
    public class EditEmployeeDtoValidator : AbstractValidator<EditEmployeeDto>
    {
        public EditEmployeeDtoValidator()
        {
            RuleFor(e => e.FullName)
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(e => e.SubdivisionId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");

            RuleFor(e => e.PositionId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");

            RuleFor(e => e.OutOfOfficeBalance)
                .GreaterThanOrEqualTo(0).WithMessage("Must be a positive value");
        }
    }
}
