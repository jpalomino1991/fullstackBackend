using FluentValidation;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstackBackend.Application.Common.Validators.Custom
{
   public class CreateEmployeeValidator : AbstractValidator<EmployeeEntity>
   {
      public CreateEmployeeValidator(IEmployeeRepository employeeRepository)
      {
         RuleFor(e => e)
            .MustAsync(async (e, _) =>
               !await employeeRepository.EmployeeExists(e))
            .WithMessage("Employee name or phone already exists");
         RuleFor(e => e.DepartmentId).GreaterThan(0);
         RuleFor(e => e.FirstName).MaximumLength(50).NotEmpty();
         RuleFor(e => e.LastName).MaximumLength(50).NotEmpty();
         RuleFor(e => e.Phone).Length(9).NotEmpty();
         RuleFor(e => e.HireDate).NotEmpty();
      }
   }
}
