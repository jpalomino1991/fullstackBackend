using AutoMapper;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using System.Globalization;

namespace fullstackBackend.Application.Common.Resolvers
{
   public class HireDateResolver : IValueResolver<EmployeeEntity, GetEmployeeResponseModel, string>
   {
      public string Resolve(EmployeeEntity source, GetEmployeeResponseModel destination, string destMember, ResolutionContext context)
      {
         string formattedHireDate = source.HireDate.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
         TimeSpan difference = DateTime.Now - source.HireDate;
         DateTime age = DateTime.MinValue + difference;
         int years = age.Year - 1;
         int months = age.Month - 1;
         int days = age.Day - 1;
         string differenceFormatted = $"({years}y-{months}m-{days}d)";
         return $"{formattedHireDate} {differenceFormatted}";
      }
   }
}
