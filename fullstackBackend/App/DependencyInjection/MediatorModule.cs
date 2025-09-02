using Autofac;
using fullstackBackend.Application.Employee.Queries;
using MediatR;
using System.Reflection;

namespace fullstackBackend.WebApi.App.DependencyInjection
{
   public class MediatorModule : Autofac.Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         // Register mediator
         builder.RegisterAssemblyTypes(typeof(IMediator)
             .GetTypeInfo().Assembly)
             .AsImplementedInterfaces();

         // Register Query & Command classes
         builder.RegisterAssemblyTypes(typeof(GetEmployeeByIdQuery).GetTypeInfo().Assembly)
             .AsClosedTypesOf(typeof(IRequestHandler<,>));

         builder.Register<ServiceFactory>(ctx =>
         {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
         });
      }
   }
}
