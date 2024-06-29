using Azure.Core;
using FluentValidation;
using OutOfOffice.Application.Project;
using OutOfOffice.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Validators
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(p => p.ProjectTypeId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");

            RuleFor(p => p.EndDate)
                .Must((request, endDate) => endDate == null || endDate >= request.StartDate)
                .WithMessage("End date must be greater than or equal start date if assigned.");

            RuleFor(p => p.ProjectManagerId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");

            RuleFor(p => p.StatusId)
                .NotEmpty().WithMessage("This field is required.")
                .GreaterThan(0).WithMessage("This field is required.");
        }
    }
}
