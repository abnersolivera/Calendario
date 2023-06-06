using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;

namespace Calendario.Servico;

public class GoogleCalendarAuthorization
{
    private readonly UserCredential _credential;

    public async Task<UserCredential> GetUserCredential()
    {
        // Carregar as informações do arquivo de credencial
        using var stream = new FileStream("credential.json", FileMode.Open, FileAccess.Read);

        // Criar a instância de GoogleAuthorizationCodeFlow
        var clientSecrets = GoogleClientSecrets.Load(stream).Secrets;
        var scopes = new[] { CalendarService.Scope.Calendar };
        var dataStore = new FileDataStore("token", true);

        var credential = await GoogleWebAuthorizationBroker
            .AuthorizeAsync(clientSecrets, scopes, "user", CancellationToken.None, dataStore)
            .ConfigureAwait(false);

        return credential;
    }
}
