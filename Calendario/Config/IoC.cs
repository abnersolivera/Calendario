using Calendario.Servico;

namespace Calendario.Config
{
    public static class IoC
    {
        public static void ConfigureIoC(this IServiceCollection services)
        {

            services.AddScoped<GoogleCalendarAuthorization>();
            services.AddScoped<GoogleCalendarService>();
            services.AddScoped(_ => "Calendario");
        }
    }
}
