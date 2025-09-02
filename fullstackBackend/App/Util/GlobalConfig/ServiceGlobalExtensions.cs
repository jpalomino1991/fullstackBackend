namespace fullstackBackend.WebApi.App.Util.GlobalConfig
{
   public static class ServiceGlobalExtensions
   {
      public static void AddGlobalSettings(this IServiceCollection services)
      {
         services.AddSingleton<IGlobalSettings, GlobalSettings>();
      }
   }
}
