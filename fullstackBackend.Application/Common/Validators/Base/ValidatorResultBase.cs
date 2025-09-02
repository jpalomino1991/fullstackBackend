using FluentValidation.Results;

namespace fullstackBackend.Application.Common.Validators.Base
{
   public class ValidatorResultBase<T> : ValidationResult
   {
      public T? Body { get; set; }
   }
}
