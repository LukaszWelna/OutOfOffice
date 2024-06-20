using FluentValidation;
using OutOfOffice.Application.Employee;
using OutOfOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Validators
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(e => e.FullName)
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("This field is required.")
                .EmailAddress().WithMessage("Must be valid email address.")
                .Custom((value, context) =>
                {
                    var employee = employeeRepository.GetEmployeeByEmailAsync(value).Result;

                    if (employee != null)
                    {
                        context.AddFailure("Email already exists in the database.");
                    }
                });

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
