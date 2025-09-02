using FluentValidation;
using fullstackBackend.Application.Common.Validators.Base;
using fullstackBackend.Application.Employee.Commands;
using fullstackBackend.Domain.Interfaces;

namespace fullstackBackend.Application.Common.Validators.Custom
{
   public class UpdateEmployeeValidator<TBody> : AbstractValidatorBase<TBody?, UpdateEmployeeCommand>
   {
      public UpdateEmployeeValidator(TBody? entity, IDepartmentRepository departmentRepository) : base(entity)
      {
         RuleFor(c => c)
            .Must((u, _) => entity != null && u.Id >= 0)
            .WithMessage("Employee doesn't exists");
         RuleFor(c => c)
            .MustAsync(async (u, _) => await departmentRepository.Exists(u.DepartmentId))
            .WithMessage("Department doesn't exists");
      }
   }
}
