using Calendario.Modelos.Interface;
using Calendario.Servico;

namespace Calendario.Config
{
    public static class IoC
    {
        public static void ConfigureIoC(this IServiceCollection services)
        {

            services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();
            services.AddScoped<IGoogleCalendarAuthorization, GoogleCalendarAuthorization>();
            services.AddScoped(_ => "Calendario");
        }
    }
}
