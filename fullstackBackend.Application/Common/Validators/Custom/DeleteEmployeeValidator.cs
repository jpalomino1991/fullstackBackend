using FluentValidation;
using fullstackBackend.Application.Common.Validators.Base;
using fullstackBackend.Application.Employee.Commands;

namespace fullstackBackend.Application.Common.Validators.Custom
{
   public class DeleteEmployeeValidator<TBody> : AbstractValidatorBase<TBody?, int>
   {
      public DeleteEmployeeValidator(TBody? entity) : base(entity)
      {
         RuleFor(c => c)
            .Must((id, _) => entity != null && id >= 0)
            .WithMessage("Employee doesn't exists");
      }
   }
}
