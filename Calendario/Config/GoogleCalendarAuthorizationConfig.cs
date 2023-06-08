using Microsoft.AspNetCore.Authentication.Cookies;

namespace Calendario.Config;

public static class GoogleCalendarAuthorizationConfig
{
    public static IServiceCollection ConfigureCalendarService(this IServiceCollection services, IConfigurationSection section)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/account/google-login";
        })
        .AddGoogle(options =>
        {            
            options.ClientId = section.GetValue<string>("client_id");
            options.ClientSecret = section.GetValue<string>("client_secret");
        });
        return services;
    }
}
