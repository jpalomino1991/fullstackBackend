using FluentValidation;
using fullstackBackend.Application.Common.Validators.Base;

namespace fullstackBackend.Application.Common.Validators.Custom
{
   public class GetEmployeeByIdValidator<TBody> : AbstractValidatorBase<TBody?, int>
   {
      public GetEmployeeByIdValidator(TBody? entity) : base(entity)
      {
         RuleFor(c => c)
            .Must((id, _) => entity != null || id < 0)
            .WithMessage("Employee doesn't exists");
      }
   }
}
