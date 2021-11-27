using EmployeeManagement.Contracts.v1.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Validators
{
    public class DeptRequestValidator : AbstractValidator<DeptRequest>
    {
        public DeptRequestValidator()
        {
            RuleFor(d => d.name)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-Z ]*$");
        }
    }
}
