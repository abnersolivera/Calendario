using Microsoft.AspNetCore.Authentication.Cookies;
namespace Calendario.Config;

public static class GoogleCalendarAuthorizationConfig
{
    public static IServiceCollection ConfigureGoogleAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
        .AddAuthentication(googleOptions =>
        {
            googleOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(googleOptions =>
        {
            googleOptions.LoginPath = "/account/google-login";
        })
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["client_id"]!;
            googleOptions.ClientSecret = configuration["client_secret"]!;
        });

        return services;
    }

}
