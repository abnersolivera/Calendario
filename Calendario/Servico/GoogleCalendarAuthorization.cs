using Calendario.Modelos.Interface;
using Calendario.Modelos.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util;
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

        var scopes = new[] { CalendarService.Scope.Calendar };

        var dataStore = new FileDataStore("token", true);

        var credential = await GoogleWebAuthorizationBroker
            .AuthorizeAsync(clientSecret, scopes, "user", CancellationToken.None, dataStore)
            .ConfigureAwait(false);

        if (credential.Token.IsExpired(SystemClock.Default))
        {
            await credential.RefreshTokenAsync(CancellationToken.None);
        }

        return credential;
    }

}
