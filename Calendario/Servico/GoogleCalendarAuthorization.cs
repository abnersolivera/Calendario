using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;

namespace Calendario.Servico;

public class GoogleCalendarAuthorization
{
    private readonly IConfiguration _configuration;


    public GoogleCalendarAuthorization()
    {
        
    }

    public GoogleCalendarAuthorization(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<UserCredential> GetUserCredential()
    {
        var credencialCalendar = _configuration.GetSection("web");
        var clientSecret = new ClientSecrets
        {
            ClientId = credencialCalendar.GetValue<string>("client_id"),
            ClientSecret = credencialCalendar.GetValue<string>("client_secret") 
        };
        
        var scopes = new[] { CalendarService.Scope.Calendar };
        var dataStore = new FileDataStore("token", true);

        var credential = await GoogleWebAuthorizationBroker
            .AuthorizeAsync(clientSecret, scopes, "user", CancellationToken.None, dataStore)
            .ConfigureAwait(false);

        return credential;
    }
}
