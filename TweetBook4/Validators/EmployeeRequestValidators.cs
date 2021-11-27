using EmployeeManagement.Contracts.v1.Requests;
using EmployeeManagement.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Validators
{
    public class EmployeeRequestValidators : AbstractValidator<EmployeeRequest>
    {
        public EmployeeRequestValidators()
        {
            RuleFor(e=> e.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .Matches("^[a-zA-Z ]*$");
            RuleFor(e=> e.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .Matches("^[a-zA-Z ]*$");
            RuleFor(e => e.DateOfBrith)
                .Matches(@"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$");
            RuleFor(e => e.Email)
                .NotNull()
                .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9_.+-]+\.[a-zA-Z0-9-.]+$");
            RuleFor(e => ((int)e.Gender))
                .NotNull()
                .ExclusiveBetween(-1, 3);
            RuleFor(e => e.DeptId)
                .NotNull();
        }
    }
}
