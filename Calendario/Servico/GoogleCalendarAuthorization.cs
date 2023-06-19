using Calendario.Modelos.Interface;
using Calendario.Modelos.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;

namespace Calendario.Servico;

public class GoogleCalendarAuthorization : IGoogleCalendarAuthorization
{
    private readonly IConfiguration _configuration;

    public GoogleCalendarAuthorization(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<UserCredential> GetUserCredential()
    {
        var credencialCalendar = _configuration.GetSection("web").Get<GoogleSecretsSettingsModel>();
        var clientSecret = new ClientSecrets
        {
            ClientId = credencialCalendar!.client_id,
            ClientSecret = credencialCalendar.client_secret
        };

        var scopes = new[] { CalendarService.Scope.Calendar };

        var credential = await GoogleWebAuthorizationBroker
            .AuthorizeAsync(clientSecret, scopes, "user", CancellationToken.None)
            .ConfigureAwait(false);

        return credential;
    }

    public async Task<UserCredential> GetUserCredentialConsole()
    {
        var credencialCalendar = _configuration.GetSection("web").Get<GoogleSecretsSettingsModel>();
        var clientSecret = new ClientSecrets
        {
            ClientId = credencialCalendar!.client_id,
            ClientSecret = credencialCalendar.client_secret
        };

        var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            clientSecret,
            new[] { CalendarService.Scope.Calendar },
            "user",
            System.Threading.CancellationToken.None,
            new FileDataStore("Tokens")
        ).Result;

        return credential;
    }
}
